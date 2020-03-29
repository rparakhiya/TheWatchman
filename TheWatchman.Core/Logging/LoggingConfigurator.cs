using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace TheWatchman.Core.Logging
{
    public static class LoggingConfigurator
    {
        public static void ConfigureLogger(HostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        }
    }
}