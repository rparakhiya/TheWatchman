using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using TheWatchman.Server.Services;

namespace TheWatchman.Server
{
    public static class ServiceConfigurator
    {
        public static void ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceConfigurator));

            services.AddSingleton<LiteDatabase>(provider =>
                new LiteDatabase(Path.Combine(Environment.CurrentDirectory, "Data", "resources.db")));

            services.AddScoped<IDbContext, LiteDbContext>();
            services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
            services.ConfigureAppConfigurations();
            services.AddScoped<IMonitoredResourceProvider, MonitoredResourceProvider>();
            services.AddScoped<IAppSettingsPersister, AppSettingsPersister>();
            services.AddScoped<ICommandProcessor, CommandProcessor>();
            services.AddScoped<IResourceMonitor, ResourceMonitor>();
        }
    }
}
