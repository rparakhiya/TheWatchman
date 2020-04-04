using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheWatchman.Server.Entities;
using TheWatchman.Server.Models;

namespace TheWatchman.Server.MappingProfiles
{
    public class ResourceStatusResolver : IValueResolver<MonitoredResource, ResourceStatus, ResourceStatuses>
    {
        public ResourceStatuses Resolve(MonitoredResource source, ResourceStatus destination, ResourceStatuses destMember, ResolutionContext context)
        {
            if (!source.LastHeartbeat.HasValue) return ResourceStatuses.Offline;

            var minutesSinceLastHeartbeat = DateTime.UtcNow.Subtract(source.LastHeartbeat.Value.ToUniversalTime()).Minutes;

            return minutesSinceLastHeartbeat <= 2 ? ResourceStatuses.Online 
                : minutesSinceLastHeartbeat <= 5 ? ResourceStatuses.Degraded
                : ResourceStatuses.Offline;
        }
    }
}
