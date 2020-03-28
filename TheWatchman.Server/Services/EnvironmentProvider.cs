using System;

namespace TheWatchman.Server.Services
{
    public class EnvironmentProvider : IEnvironmentProvider
    {
        public string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
    }
}