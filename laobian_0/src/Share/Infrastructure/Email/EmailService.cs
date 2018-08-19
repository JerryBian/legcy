using System;
using System.Text;
using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Config;
using Laobian.Share.Infrastructure.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Laobian.Share.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly ConfigSetting _configSetting;

        public EmailService(ConfigSetting configSetting)
        {
            _configSetting = configSetting;
        }

        public async Task SendAsync(EmailMessage message)
        {
            var client = new SendGridClient(_configSetting.SendGridApiKey);
            var html = new StringBuilder();
            html.AppendLine($"<p>{message.Message}</p>");

            if (!string.IsNullOrEmpty(message.RequestIp))
                html.AppendLine($"<p>IP Address: <strong>{message.RequestIp}</strong></p>");

            if (!string.IsNullOrEmpty(message.RequestHeader))
                html.AppendLine(CreateRequestHeadersHtml(message.RequestHeader));

            var level = "info";
            if (message.Exception != null)
            {
                level = "error";
                html.AppendLine(CreateExceptionHtml(message.Exception));
            }

            var sendGridMsg = new SendGridMessage
            {
                From = new EmailAddress("robot@laobian.me", "Laobian Agent"),
                Subject = $"[{level}] @ {message.EmailDomain}",
                HtmlContent = html.ToString()
            };
            sendGridMsg.AddTo(_configSetting.AdminEmail, "Webmaster");
            await client.SendEmailAsync(sendGridMsg);
        }

        private string CreateExceptionHtml(Exception ex)
        {
            return $@"<details>
<summary>Exception Details</summary>
   <pre><code>{ex}</code></pre></details>";
        }

        private string CreateRequestHeadersHtml(string headers)
        {
            return $@"<details>
<summary>Request Headers</summary>
   <pre><code>{headers}</code></pre></details>";
        }
    }
}