using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheWatchman.Server.Services;

namespace TheWatchman.Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0].Equals("--start"))
            {
                ServerBootstrapper.Bootstrap(args);
            }
            else
            {
                // --------------------- Setup ---------------------
                var services = new ServiceCollection();
                services.ConfigureCoreServices();
                var provider = services.BuildServiceProvider();

                // --------------------------------------------------
                var commandProcessor = provider.GetService<ICommandProcessor>();

                commandProcessor.ExecuteCommand(args);
            }
        }
    }
}