using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using TheWatchman.Server.Options;
using TheWatchman.Server.Services.Email;
using TheWatchman.Server.Services.Notifiers.Messages;

namespace TheWatchman.Server.Services.Notifiers
{
    public class ResourceStatusChangedEmailNotifier : EmailNotifier<ResourceStatusChangedNotification>
    {
        private readonly EmailNotificationOptions _emailNotificationOptions;

        public ResourceStatusChangedEmailNotifier(IEmailServer emailServer, EmailNotificationOptions emailNotificationOptions)
            : base(emailServer)
        {
            _emailNotificationOptions = emailNotificationOptions;
        }

        protected override MimeMessage PrepareMessage(ResourceStatusChangedNotification notification)
        {
            return new MimeMessage(_emailNotificationOptions.From, _emailNotificationOptions.To.Select(x => new MailboxAddress(x)))
            {
                Subject = $"Resource Status Alarm: {notification.Resource.Name} - ({notification.Status})",
                Body = new TextPart("Plain")
                {
                    Text = $"Status: ${notification.Status}\n" +
                           $"Time: ${notification.Time:g}" 
                }
            };
        }
    }
}