using Laobian.Infrastuture.Const;
using Laobian.Infrastuture.Entity.Log;
using Laobian.Infrastuture.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Laobian.Blog.Controllers
{
    public class BlogControllerBase : Controller
    {
        public readonly ILog4BlogVisitTask _log4BlogVisitTask;

        public BlogControllerBase(ILog4BlogVisitTask log4BlogVisitTask)
        {
            _log4BlogVisitTask = log4BlogVisitTask;
        }

        public virtual void AddBlogVisitLog(BlogComponent component, string pageId = null)
        {
            var log = GetLog4BlogVisit();
            log.Component = component.ToString();
            log.PageId = pageId;
            _log4BlogVisitTask.Add(log);
        }

        private Log4BlogVisit GetLog4BlogVisit()
        {
            return new Log4BlogVisit {
                CreateTime = DateTime.UtcNow,
                RemoteAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                RequestHeader = string.Join(Environment.NewLine, HttpContext.Request.Headers.Select(_ => $"{_.Key}: {_.Value}")),
                RequestUrl = HttpContext.Request.Host.Value,
                RequestBody = new StreamReader(HttpContext.Request.Body).ReadToEnd(),
                StackTrace = Environment.StackTrace,
                UpdateTime = DateTime.UtcNow,
                ThreadId = Environment.CurrentManagedThreadId,
                HostName = HostComponent.Blog.ToString()
            };
        }
    }
}
