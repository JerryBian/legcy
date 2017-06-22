using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Const;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Model;

namespace Laobian.Blog.Controllers
{
    public class HomeController : BlogControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly AppSettings _settings;

        public HomeController(
            IOptions<AppSettings> settings, 
            IBlogService blogService, 
            ILog4BlogVisitTask log4BlogVisitTask) : base(log4BlogVisitTask)
        {
            _blogService = blogService;
            _settings = settings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _blogService.GetAllPostsAsync(true);

            AddBlogVisitLog(BlogComponent.Home);
            return View(model);
        }

        [Route("/tag/{tag}")]
        public async Task<IActionResult> Tag([FromRoute]string tag)
        {
            var model = await _blogService.GetPostsByTag(tag);

            AddBlogVisitLog(BlogComponent.Tag, tag);
            return View("Index", model);
        }

        [Route("/rss")]
        public async Task<IActionResult> Rss()
        {
            var model = await _blogService.GetAllPostsAsync(true);

            AddBlogVisitLog(BlogComponent.Rss);
            return View(model);
        }

        [HttpGet]
        [Route("/{year:int}/{month:int}/{url}.html")]
        public async Task<IActionResult> Post([FromRoute]int year, [FromRoute]int month, [FromRoute]string url, [FromQuery]string lang)
        {
            var model = await _blogService.GetPostByUrl(url);
            if(model == null)
            {
                return NotFound();
            }

            ViewBag.Lang = "en";
            if(string.Equals(lang, "ch", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(model.HtmlContentCh))
                {
                    return Redirect($"/{model.FullUrl}");
                }

                ViewBag.Lang = "ch";
            }
            else
            {
                if(string.IsNullOrEmpty(model.HtmlContentEn) && !string.IsNullOrEmpty(model.HtmlContentCh))
                {
                    return Redirect($"/{model.FullUrl}?lang=ch");
                }

                if (!string.IsNullOrEmpty(lang))
                {
                    return Redirect($"/{model.FullUrl}");
                }
            }

            AddBlogVisitLog(BlogComponent.Post, model.Id.ToString());
            return View(model);
        }
    }
}
