using System;
using System.Collections.Generic;
using System.Text;

namespace TheWatchman.Monitor.Models
{
    public class ResourceMonitorOptions
    {
        public string ResourceId { get; set; }
        public string Secret { get; set; }
        public string ServerUrl { get; set; }
        public int TokenLifetime { get; set; }
    }
}
