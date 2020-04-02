using System;

namespace TheWatchman.Server.Services
{
    public interface ICommandProcessor
    {
        void ExecuteCommand(string[] args);
    }

    public class CommandProcessor : ICommandProcessor
    {
        private readonly IMonitoredResourceProvider _monitoredResourceProvider;

        public CommandProcessor(IMonitoredResourceProvider monitoredResourceProvider)
        {
            _monitoredResourceProvider = monitoredResourceProvider;
        }
        
        private const string Help = "help";
        private const string Register = "register";
        
        public void ExecuteCommand(string[] args)
        {
            var command = args[0].Substring(2);
            
            switch (command)
            {
                case Help:
                    ExecuteHelp();
                    break;
                case Register:
                    ExecuteRegister();
                    break;
            }
        }

        private void ExecuteRegister()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null.", nameof(name));
            }
            
            Console.Write("Description: ");
            var description = Console.ReadLine();

            var resource = _monitoredResourceProvider.Register(name, description);

            Console.WriteLine($"Resource Id: {resource.Id}");
            Console.WriteLine($"Secret: {resource.Secret}");
        }

        private void ExecuteHelp()
        {
            Console.WriteLine("Possible Arguments:");
            Console.WriteLine("\t--help - Shows help");
            Console.WriteLine("\t--register - Registers a new resource");
        }
    }
}