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
        private const string Delete = "delete";
        
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
                case Delete:
                    ExecuteDelete();
                    break;
            }
        }

        private void ExecuteDelete()
        {
            Console.Write("Resource Id: ");
            var resourceId = Console.ReadLine();

            var resource = _monitoredResourceProvider.GetResource(resourceId);
            if (resource == null)
            {
                throw new ArgumentException("Invalid resourceId");
            }

            Console.WriteLine("Resource:");
            Console.WriteLine(resource);
            Console.WriteLine("Are you sure you want to delete the resource? (Y/n): ");
            if (Console.ReadLine().Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                _monitoredResourceProvider.Delete(resourceId);
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
            Console.WriteLine("\t--delete - Delete a resource");
        }
    }
}