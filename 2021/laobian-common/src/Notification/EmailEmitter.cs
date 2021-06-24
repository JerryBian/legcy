using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Laobian.Common.Base;
using Laobian.Common.Config;
using Laobian.Common.Setting;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Laobian.Common.Notification
{
    /// <summary>
    /// Implementation of <see cref="IEmailEmitter"/>
    /// </summary>
    public class EmailEmitter : IEmailEmitter
    {
        private readonly string _emailTemplate;

        /// <summary>
        /// Constructor of <see cref="EmailEmitter"/>
        /// </summary>
        public EmailEmitter()
        {
            _emailTemplate = @"<!DOCTYPE html>

<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width'>
    <style type='text/css'>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
            font-size: 1rem;
        }}

        #footer {{
            text-align: center;
            padding: 1%;
            margin: 1rem auto;
            color: #999999;
            font-size: 12px;
        }}

        table {{
            border-collapse: separate;
            width: 100%;
            table-layout: fixed;
        }}

        table tr td{{
            word-wrap: break-word;
        }}

        table tr td:first-child {{
            width: auto;
            color: darkblue;
            max-width: 30%;
        }}

        table tr td:nth-child(2) {{
            font-family: 'Courier New', Courier, monospace;
            padding-left: 0.5rem;
        }}

        #message {{
            background-color: snow;
            padding: 1%;
            margin: 1% auto;
        }}

        #system_status {{
            background-color: whitesmoke;
            margin: 1% auto;
            padding: 1%;
        }}

        .time {{
            font-family: Arial;
        }}

        details {{ 
            margin: 1rem auto; 
        }}
    </style>
</head>

<body>
    <p>Dear Administrator,</p>

    <p>We prepared this message for you as requested by <strong>laobian</strong> services, please take a look in case of something
        important you <i>might</i> be interested at.</p>

    <h4>Message</h4>

    <div id='message'>
        {0}
    </div>

    <h4>System status</h4>

    <div id='system_status'>
        <table>
            <tbody>
                <tr>
                    <td>Startup at:</td>
                    <td>{1}</td>
                </tr>
                <tr>
                    <td>Process id:</td>
                    <td>{2}</td>
                </tr>
                <tr>
                    <td>User name:</td>
                    <td>{3}</td>
                </tr>
                <tr>
                    <td>Machine name:</td>
                    <td>{4}</td>
                </tr>
                <tr>
                    <td>Allocated memory:</td>
                    <td>{5}</td>
                </tr>
                <tr>
                    <td>CPU time:</td>
                    <td>{6}</td>
                </tr>
                <tr>
                    <td>OS version:</td>
                    <td>{7}</td>
                </tr>
                <tr>
                    <td>Processor count:</td>
                    <td>{8}</td>
                </tr>
                <tr>
                    <td>CLR version:</td>
                    <td>{9}</td>
                </tr>
                <tr>
                    <td>Assembly version:</td>
                    <td>{10}</td>
                </tr>

            </tbody>
        </table>
    </div>

    <div id='footer'>
        <p>&copy; Jerry Bian, generated at <span class='time'>{11}</span></p>
    </div>
</body>";
        }

        #region Implementation of IEmailEmitter

        /// <summary>
        /// Emit errors
        /// </summary>
        /// <param name="htmlMessage">HTML message</param>
        /// <param name="exception">Exception attached</param>
        /// <returns>Whether emit successfully or not</returns>
        public async Task<bool> EmitErrorAsync(string htmlMessage, Exception exception = null)
        {
            if (exception != null)
            {
                htmlMessage += $"<details open><summary>Exception</summary><pre>{exception}</pre></details>";
            }

            var message = string.Format(GetEmailTemplate(), htmlMessage);
            return await EmitAsync("Please check ERROR!", message);
        }

        /// <summary>
        /// Emit healthy status
        /// </summary>
        /// <param name="htmlMessage">HTML message</param>
        /// <returns>Whether emit successfully or not</returns>
        public async Task<bool> EmitStatusAsync(string htmlMessage)
        {
            var message = string.Format(GetEmailTemplate(), htmlMessage);
            return await EmitAsync("Please check status ...", message);
        }

        /// <summary>
        /// Emit information
        /// </summary>
        /// <param name="htmlMessage">HTML message</param>
        /// <returns>Whether emit successfully or not</returns>
        public async Task<bool> EmitInfoAsync(string htmlMessage)
        {
            var message = string.Format(GetEmailTemplate(), htmlMessage);
            return await EmitAsync("New activity...", message);
        }

        #endregion

        private async Task<bool> EmitAsync(string subject, string html)
        {
            if (string.IsNullOrEmpty(AppConfig.Default.SendGridKey) || string.IsNullOrEmpty(AppSetting.Default.AdminEmail))
            {
                return false;
            }

            var client = new SendGridClient(AppConfig.Default.SendGridKey);
            var from = new EmailAddress(AppSetting.Default.NotifyEmail, AppSetting.Default.NotifyName);
            var to = new EmailAddress(AppSetting.Default.AdminEmail, AppSetting.Default.AdminFullName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, html, html);
            var response = await client.SendEmailAsync(msg);
            return response.StatusCode == HttpStatusCode.OK;
        }

        private string GetEmailTemplate()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var template = string.Format(
                    _emailTemplate,
                    "{0}",
                    SystemState.StartupTime.ToChinaTime().ToIso8601(),
                    process.Id,
                    Environment.UserName,
                    Environment.MachineName,
                    FileSizeHelper.Format(process.WorkingSet64),
                    process.TotalProcessorTime,
                    Environment.OSVersion,
                    Environment.ProcessorCount,
                    Environment.Version,
                    Assembly.GetEntryAssembly().GetName().Version,
                    DateTime.UtcNow.ToChinaTime().ToIso8601());
                template = template.Replace("{", "{{").Replace("}", "}}").Replace("{{0}}", "{0}");
                return template;
            }
        }
    }
}
