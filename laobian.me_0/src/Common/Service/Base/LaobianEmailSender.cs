using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Model;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Laobian.Service.Base
{
    public class LaobianEmailSender:ILaobianEmailSender
    {
        private readonly AppSettings _setting;

        public LaobianEmailSender(IOptions<AppSettings> setting)
        {
            _setting = setting.Value;
        }

        public async Task SendAsync(EmailMessage message)
        {
            var client = new SendGridClient(_setting.SendGridApiKey);
            var msg = new SendGridMessage {
                From = new EmailAddress(message.FromAddress, message.FromName),
                HtmlContent = message.HtmlContent,
                Subject = message.Subject
            };
            msg.AddTo(new EmailAddress(message.ToAddress, message.ToName));
            await client.SendEmailAsync(msg);
        }
    }
}
