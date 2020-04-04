using System;
using TheWatchman.Server.Models;

namespace TheWatchman.Server.Services.Notifiers.Messages
{
    public class NotificationBase
    {
        public MonitoredResourceModel Resource { get; set; }
        public DateTime Time { get; set; }
    }
}