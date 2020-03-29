using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TheWatchman.Monitor.Services;

namespace TheWatchman.Monitor
{
    public class HeartbeatServiceWorker : IHostedService, IDisposable
    {
        private readonly ILogger<HeartbeatServiceWorker> _logger;
        private readonly IHeartbeatService _heartbeatService;
        private Timer _timer;

        public HeartbeatServiceWorker(ILogger<HeartbeatServiceWorker> logger, IHeartbeatService heartbeatService)
        {
            _logger = logger;
            _heartbeatService = heartbeatService;
        }

        private void RegisterHeartbeat(object state)
        {
            _logger.LogInformation("Sending heartbeat at: {time}", DateTimeOffset.Now);

            try
            {
                _heartbeatService.RegisterHeartbeatAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending heartbeat");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            _timer = new Timer(RegisterHeartbeat, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
