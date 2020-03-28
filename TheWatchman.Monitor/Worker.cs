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
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHeartbeatService _heartbeatService;

        public Worker(ILogger<Worker> logger, IHeartbeatService heartbeatService)
        {
            _logger = logger;
            _heartbeatService = heartbeatService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // TODO: Making this method wait doesn't start the windows service and gets stuck in the starting mode. Need to fix

            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Sending heartbeat at: {time}", DateTimeOffset.Now);

                    try
                    {
                        await _heartbeatService.RegisterHeartbeatAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while sending heartbeat");
                    }

                    await Task.Delay(60000, stoppingToken);
                }
            }
        
            _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
        }
    }
}
