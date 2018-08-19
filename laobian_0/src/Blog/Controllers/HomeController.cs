using System;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Blog.Models;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Mvc.Head;
using Laobian.Share.Utility.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;

        public HomeController(IBlogService blogService, ISettingService settingService)
        {
            _blogService = blogService;
            _settingService = settingService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index([FromQuery] int p)
        {
            var headItem = (HeadItem)HttpContext.Items[HeadItem.HeadItemKey];
            var results = await _blogService.FindAllPostAsync(true);
            results = results.OrderByDescending(_ => _.CreateAt).ToList();
            var paginationChunkSize = await _settingService.FindAsync<int>(SettingKey.BlogPaginationChunkSize);
            var pagination = new Pagination(p, (int)Math.Ceiling(results.Count() / (double)paginationChunkSize));
            var posts = results.ToPagedObjects(paginationChunkSize, pagination.CurrentPage);

            if (pagination.CurrentPage > 1)
            {
                ViewData["Title"] = $"Home (Page {pagination.CurrentPage})";
                ViewData["Path"] = $"/?p={p}";
                headItem.PageTitle = $"Page {pagination.CurrentPage}";
                headItem.Robots = "noindex,nofollow";
            }
            else
            {
                headItem.Canonical = "/";
                headItem.Description = await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription);
            }

            var randomPostItem = (RandomPostFactor)HttpContext.Items[RandomPostFactor.ItemKey];
            randomPostItem.ExcludePosts = posts.Select(_ => _.Id);

            return View(new PagedBlogPostViewModel { Pagination = pagination, Posts = posts, Url = Request.Path });
        }

        [HttpGet]
        [Route("/about")]
        [ResponseCache(Duration = 60 * 60 * 24, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> About()
        {
            var headItem = (HeadItem)HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "About";
            headItem.Canonical = "/about";
            headItem.Description = $"About - {await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)}";

            return View();
        }

        [HttpGet]
        [Route("/rss")]
        [ResponseCache(Duration = 60 * 60 * 12, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Rss()
        {
            var rss = await _blogService.BuildRssAsync();
            return Content(rss, "application/rss+xml");
        }

        [HttpGet]
        [Route("sitemap")]
        [Route("sitemap.xml")]
        [ResponseCache(Duration = 60 * 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Sitemap()
        {
            var sitemap = await _blogService.BuildSitemapAsync();
            return Content(sitemap, "application/xml");
        }
    }
}