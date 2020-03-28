using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheWatchman.Server.Entities;
using TheWatchman.Server.Models;

namespace TheWatchman.Server.Services
{
    public class ResourceMonitor : IResourceMonitor
    {
        private readonly IMapper _mapper;
        private readonly IMonitoredResourceProvider _monitoredResourceProvider;
        private readonly IDbContext _context;

        public ResourceMonitor(IMapper mapper, IMonitoredResourceProvider monitoredResourceProvider, IDbContext context)
        {
            _mapper = mapper;
            _monitoredResourceProvider = monitoredResourceProvider;
            _context = context;
        }

        public Task RegisterHeartbeat(string resourceId)
        {
            var resource = _monitoredResourceProvider.GetResource(resourceId);
            resource.LastHeartbeat = DateTime.UtcNow;

            _context.Update<MonitoredResource, string>(resource);

            return Task.CompletedTask;
        }

        public Task<List<ResourceStatus>> GetResourceStatus()
        {
            return Task.FromResult(_mapper.Map<List<ResourceStatus>>(_monitoredResourceProvider.GetResources()));
        }

        public Task<ResourceStatus> GetResourceStatus(string resourceId)
        {
            var resource = _monitoredResourceProvider.GetResource(resourceId);

            return Task.FromResult(_mapper.Map<ResourceStatus>(resource));
        }
    }
}
