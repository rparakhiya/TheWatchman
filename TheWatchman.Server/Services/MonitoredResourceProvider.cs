using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Microsoft.Extensions.Configuration;
using TheWatchman.Server.Entities;
using TheWatchman.Server.Services;

namespace TheWatchman.Server.Services
{
    public class MonitoredResourceProvider : IMonitoredResourceProvider
    {
        private const string AppSettingsKey = "Resources";
        
        private readonly IDbContext _context;

        public MonitoredResourceProvider(IDbContext context)
        {
            _context = context;
        }
        
        public List<MonitoredResource> GetResources()
        {
            return _context.GetAll<MonitoredResource, string>();
        }

        public MonitoredResource GetResource(string id)
        {
            return _context.Get<MonitoredResource, string>(id);
        }

        public MonitoredResource Register(string name, string description)
        {
            var resource = new MonitoredResource()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Description = description,
                Secret = Guid.NewGuid().ToString()
            };

            _context.Insert<MonitoredResource, string>(resource);
            
            return resource;
        }
    }
}