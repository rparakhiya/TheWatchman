using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit;
using TheWatchman.Server.Services.Email;
using TheWatchman.Server.Services.Notifiers.Messages;

namespace TheWatchman.Server.Services.Notifiers
{
    public abstract class EmailNotifier<TNotification> : INotifier<TNotification>
        where TNotification : NotificationBase
    {
        private readonly IEmailServer _emailServer;

        public EmailNotifier(IEmailServer emailServer)
        {
            _emailServer = emailServer;
        }

        public virtual async Task Notify(TNotification notification)
        {
            var message = PrepareMessage(notification);
            await SendEmailAsync(message);
        }

        protected abstract MimeMessage PrepareMessage(TNotification notification);

        protected virtual async Task SendEmailAsync(MimeMessage message)
        {
            await _emailServer.SendEmailAsync(message);
        }
    }
}