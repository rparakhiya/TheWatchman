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

        public override string ToString()
        {
            var mostRecentHeartbeat = this.LastHeartbeat.HasValue
                ? this.LastHeartbeat.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : "Never";
            
            return $"Id: {this.Id}\n" +
                   $"Name: {this.Name}\n" +
                   $"Description: {this.Description}\n" +
                   $"Most Recent Heartbeat: {mostRecentHeartbeat}";
        }
    }
}