using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace TheWatchman.Monitor.RestConfigurations
{
    public class TOtpAuthenticator : AuthenticatorBase
    {
        public TOtpAuthenticator(string token)
            : base(token)
        {
        }

        protected override Parameter GetAuthenticationParameter(string accessToken)
        {
            return new Parameter("Authorization", $"totp {accessToken}", ParameterType.HttpHeader);
        }
    }
}
