using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWatchman.Server.Models;

namespace TheWatchman.Server.Services
{
    public interface IResourceMonitor
    {
        Task RegisterHeartbeat(string resourceId);
        Task<List<ResourceStatus>> GetResourceStatus();
        Task<ResourceStatus> GetResourceStatus(string resourceId);
    }
}
