using System.Threading.Tasks;
using TheWatchman.Server.Services.Notifiers.Messages;

namespace TheWatchman.Server.Services.Notifiers
{
    public interface INotifier<in TNotification>
        where TNotification : NotificationBase
    {
        Task Notify(TNotification notification);
    }
}