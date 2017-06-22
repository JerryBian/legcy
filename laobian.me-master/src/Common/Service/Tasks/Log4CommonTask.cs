using Laobian.Infrastuture.Entity.Log;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laobian.Service.Tasks
{
    public class Log4CommonTask : TaskBase<Log4Common>, ILog4CommonTask
    {
        private readonly AppSettings _settings;
        private readonly ILaobianEmailSender _emailSender;
        private readonly ILog4CommonRepository _log4CommonRepository;

        public Log4CommonTask(
            IOptions<AppSettings> settings,
            ILaobianEmailSender emailSender,
            ILog4CommonRepository log4CommonRepository)
        {
            _settings = settings.Value;
            _emailSender = emailSender;
            _log4CommonRepository = log4CommonRepository;
        }

        public override void DoWork()
        {
            DoWork(() =>
            {
                if (TryDequeueAll(out List<Log4Common> elements))
                {
                    _log4CommonRepository.AddAsync(elements);
                    SendEmailAsync(elements).Wait();
                    return true;
                }

                return false;
            });
        }

        private async Task SendEmailAsync(List<Log4Common> logs)
        {
            foreach(var log in logs)
            {
                var email = new EmailMessage {
                    FromAddress = "robot@laobian.me",
                    FromName = $"robot",
                    HtmlContent = GetEmailMessage(log),
                    Subject = $"❤️-{log.HostName}",
                    ToAddress = _settings.AdminEmail,
                    ToName = _settings.AdminFullName
                };
                await _emailSender.SendAsync(email);
            }
        }

        private string GetEmailMessage(Log4Common log)
        {
            var result = new StringBuilder();
            result.AppendLine($"<ul>");
            result.AppendLine($"<li>Host: {log.HostName}</li>");
            result.AppendLine($"<li>Ip: {log.RemoteAddress}</li>");
            result.AppendLine($"<li>Request URL: {log.RequestUrl}</li>");
            result.AppendLine("</ul>");
            result.AppendLine($"<pre>{log.Message}</pre>");
            result.AppendLine("<hr/>");
            result.AppendLine($"<blockquote><pre>{log.RequestHeader}</pre></blockquote>");
            
            return result.ToString();
        }
    }
}
