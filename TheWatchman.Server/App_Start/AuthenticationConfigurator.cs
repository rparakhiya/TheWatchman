using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheWatchman.Server.Authentication;
using TheWatchman.Server.Options;

namespace TheWatchman.Server
{
    public static class AuthenticationConfigurator
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("totp")
                .AddScheme<TOtpAuthenticationOptions, TOtpAuthenticationHandler>("totp", (options) =>
                {
                    options.TokenLifetime = configuration.GetValue<int>("Authentication:TOtp:TokenLifetime");
                });
        }
    }
}
