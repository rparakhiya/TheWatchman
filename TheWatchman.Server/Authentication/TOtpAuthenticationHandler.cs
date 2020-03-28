using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtpNet;
using TheWatchman.Core;
using TheWatchman.Server.Options;
using TheWatchman.Server.Services;

namespace TheWatchman.Server.Authentication
{
    public class TOtpAuthenticationHandler : AuthenticationHandler<TOtpAuthenticationOptions>
    {
        public const string TOtpAuthenticationScheme = "totp";

        private readonly IMonitoredResourceProvider _resourceProvider;

        public TOtpAuthenticationHandler(IOptionsMonitor<TOtpAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IMonitoredResourceProvider resourceProvider)
            : base(options, logger, encoder, clock)
        {
            _resourceProvider = resourceProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.RouteValues.TryGetValue("resourceId", out var resourceId))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Auth Scheme configuration. No route parameter found with name 'resourceId'."));
            }

            var authHeader = Request.Headers["Authorization"];
            if (authHeader.Count == 0)
            {
                return Task.FromResult(AuthenticateResult.Fail("No Auth token found."));
            }

            var authValues = authHeader.First().Split(" ");
            if (authValues.Length != 2 || !authValues.First().Equals(TOtpAuthenticationScheme, StringComparison.InvariantCultureIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid auth scheme."));
            }

            var otp = authValues[1];

            var resource = _resourceProvider.GetResource(resourceId.ToString());

            var totpGenerator = new TOtpGenerator(resource.Secret, Options.TokenLifetime);
            var validOtp = totpGenerator.GetOtp();

            if (!otp.Equals(validOtp))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }

            return Task.FromResult(AuthenticateResult.Success(
                new AuthenticationTicket(new GenericPrincipal(new GenericIdentity(resource.Id.ToString()), new String[] { }),
                    TOtpAuthenticationScheme)));

        }
    }
}
