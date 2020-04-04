using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheWatchman.Server.Options;
using TheWatchman.Server.Services;
using TheWatchman.Server.Services.Email;

namespace TheWatchman.Server
{
    public static class AppConfigurationConfigurator
    {
        public static void ConfigureAppConfigurations(this IServiceCollection services)
        {
            var environment = services.BuildServiceProvider().GetService<IEnvironmentProvider>().GetEnvironment();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables("WATCHMAN_")
                .Build();

            services.AddSingleton(configuration);

            var smtpConfiguration = configuration.GetSection("Smtp").Get<SmtpConfiguration>();
            var notificationConfiguration = configuration.GetSection("Notifications").Get<NotificationOptions>();

            services.AddSingleton(smtpConfiguration);
            services.AddSingleton(notificationConfiguration);
        }
    }
}
