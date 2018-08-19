using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Laobian.Shared.Interface.Blog;
using Laobian.Shared.Interface;
using Laobian.Shared.Interface.Options;
using Microsoft.Extensions.Options;
using Laobian.Shared.Blog;

namespace Laobian.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly BlogConfiguration _config;

        public HomeController(IBlogService blogService, IOptions<BlogConfiguration> config)
        {
            _blogService = blogService;
            _config = config.Value;
        }

        public async Task<IActionResult> Index([FromQuery] int p)
        {
            var results = await _blogService.GetBlogPostsAsync();
            var pagination = new Pagination(p, (int)Math.Ceiling(results.Count / (double)_config.PaginationChunkSize));
            var posts = results.ToPagedObjects(_config.PaginationChunkSize, pagination.CurrentPage);

            if (pagination.CurrentPage > 1)
            {
                ViewData["Title"] = $"Home (Page {pagination.CurrentPage})";
                ViewData["Path"] = $"/?p={p}";
            }

            return View(new PagedBlogPosts { Pagination = pagination, Posts = posts, Url = Request.Path });
        }

        [HttpGet]
        [Route("/about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("/rss")]
        public async Task<IActionResult> Rss()
        {
            var rss = await _blogService.BuildRssAsync();
            return Content(rss, "application/rss+xml");
        }

        [HttpGet]
        [Route("sitemap")]
        [Route("sitemap.xml")]
        public async Task<IActionResult> Sitemap()
        {
            var sitemap = await _blogService.BuildSitemapAsync();
            return Content(sitemap, "application/xml");
        }
    }
}
