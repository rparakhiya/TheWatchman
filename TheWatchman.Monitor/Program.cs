using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheWatchman.Monitor.Models;
using TheWatchman.Monitor.Services;

namespace TheWatchman.Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                    {
                        builder
                            .SetBasePath(Environment.CurrentDirectory)
                            .AddJsonFile("appsettings.json", true);
                    })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

                    services.AddSingleton(configuration.GetSection("ResourceMonitor").Get<ResourceMonitorOptions>());
                    services.AddSingleton<IHeartbeatService, HeartbeatService>();
                    services.AddHostedService<Worker>();
                });
    }
}
