using Laobian.Infrastuture.Interface.Service;
using Microsoft.AspNetCore.Builder;
using System;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Laobian.Infrastuture.Entity.Log;
using Laobian.Infrastuture.Const;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq;

namespace Laobian.Infrastuture.Middleware
{
    public class ExceptionMiddleware
    {
        public static void Use(IApplicationBuilder app, ILog4CommonTask log4CommonTask, HostComponent hostComponent)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "text/html";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var log = new Log4Common
                        {
                            CreateTime = DateTime.UtcNow,
                            HostName = HostComponent.Blog.ToString(),
                            Level = LogLevel.Error.ToString(),
                            Message = error.Error.ToString(),
                            RemoteAddress = context.Connection.RemoteIpAddress.ToString(),
                            RequestBody = new StreamReader(context.Request.Body).ReadToEnd(),
                            RequestHeader = string.Join(Environment.NewLine, context.Request.Headers.Select(_ => $"{_.Key}: {_.Value}")),
                            RequestUrl = context.Request.GetEncodedUrl(),
                            StackTrace = Environment.StackTrace,
                            ThreadId = Environment.CurrentManagedThreadId,
                            UpdateTime = DateTime.UtcNow
                        };

                        log4CommonTask.Add(log);
                        var data = Encoding.UTF8.GetBytes("<pre>Not found.</pre><p>Please contact JerryBian@outlook.com, thank you!</p>");
                        await context.Response.Body.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                    }
                });
            });
        }
    }
}
