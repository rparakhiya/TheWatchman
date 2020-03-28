using System;

namespace TheWatchman.Server.Entities
{
    public class MonitoredResource : IPersistableEntity<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Secret { get; set; }

        public DateTime? LastHeartbeat { get; set; }
    }
}