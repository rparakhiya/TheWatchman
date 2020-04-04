using TheWatchman.Server.Models;

namespace TheWatchman.Server.Services.Notifiers.Messages
{
    public class ResourceStatusChangedNotification : NotificationBase
    {
        public ResourceStatus Status { get; set; }
    }
}