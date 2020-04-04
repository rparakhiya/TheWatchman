using System;
using System.Collections.Generic;
using TheWatchman.Server.Entities;

namespace TheWatchman.Server.Services
{
    public interface IMonitoredResourceProvider
    {
        List<MonitoredResource> GetResources();
        MonitoredResource GetResource(string id);
        MonitoredResource Register(string name, string description);
        MonitoredResource Delete(string resourceId);
    }
}