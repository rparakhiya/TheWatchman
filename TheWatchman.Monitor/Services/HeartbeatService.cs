using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using TheWatchman.Core;
using TheWatchman.Core.Authentication;
using TheWatchman.Monitor.Models;
using TheWatchman.Monitor.RestConfigurations;

namespace TheWatchman.Monitor.Services
{
    public class HeartbeatService : IHeartbeatService
    {
        private readonly ResourceMonitorOptions _resourceMonitorOptions;
        private readonly TOtpGenerator _tOtpGenerator;

        public HeartbeatService(ResourceMonitorOptions resourceMonitorOptions)
        {
            _resourceMonitorOptions = resourceMonitorOptions;
            _tOtpGenerator = new TOtpGenerator(_resourceMonitorOptions.Secret, _resourceMonitorOptions.TokenLifetime);
        }

        public async Task RegisterHeartbeatAsync()
        {
            var client = new RestClient(_resourceMonitorOptions.ServerUrl)
            {
                Authenticator = new TOtpAuthenticator(_tOtpGenerator.GetOtp())
            };

            var request = new RestRequest($"api/resources/{_resourceMonitorOptions.ResourceId}/heartbeat", Method.POST);
            await client.PostAsync<object>(request);
        }
    }
}
