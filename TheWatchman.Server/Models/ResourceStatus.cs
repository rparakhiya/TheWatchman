using System;
using TheWatchman.Server.Entities;

namespace TheWatchman.Server.Models
{
    public class ResourceStatus
    {
        public MonitoredResourceModel Resource { get; set; }
        public ResourceStatuses Status { get; set; }
        public DateTime? LastHeartbeat { get; set; }
    }
}
