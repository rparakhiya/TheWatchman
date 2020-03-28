using System;
using System.Text;
using System.Text.Unicode;
using Microsoft.Extensions.Configuration;
using OtpNet;

namespace TheWatchman.Server.Services
{
    public class RequestAuthenticator : IRequestAuthenticator
    {
        private readonly IMonitoredResourceProvider _monitoredResourceProvider;

        public RequestAuthenticator(IMonitoredResourceProvider monitoredResourceProvider)
        {
            _monitoredResourceProvider = monitoredResourceProvider;
        }
        
        public bool Authenticate(string id, string auth)
        {
            var resource = _monitoredResourceProvider.GetResource(id);
            var totp = new Totp(Encoding.UTF8.GetBytes(resource.Secret));
            
            return totp.ComputeTotp().Equals(auth);
        }
    }
}