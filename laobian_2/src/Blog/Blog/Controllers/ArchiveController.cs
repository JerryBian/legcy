using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Blog.Models;
using Laobian.Shared.Blog;
using Laobian.Shared.Interface;
using Laobian.Shared.Interface.Blog;
using Laobian.Shared.Interface.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Laobian.Blog.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly BlogConfiguration _config;

        public ArchiveController(IBlogService blogService, IOptions<BlogConfiguration> config)
        {
            _blogService = blogService;
            _config = config.Value;
        }

        [HttpGet]
        [Route("/archives")]
        public async Task<IActionResult> Index()
        {
            var result = await _blogService.GetBlogPostsAsync();
            var model = new List<ArchivesViewModel>();
            foreach(var item in result.GroupBy(_ => _.GetByLangWithDefault().CreateTime.ToYear()))
            {
                model.Add(new ArchivesViewModel {
                    Key = item.Key,
                    Posts = item.ToList()
                });
            }

            ViewData["Title"] = "Archives";
            return View(model);
        }

        [HttpGet]
        [Route("/archive/{year:int}")]
        public async Task<IActionResult> GetPostsByTag([FromRoute]int year,[FromQuery] int p)
        {
            var title = $"Archive: {year}";
            var path = $"/archive/{year}";

            var result = await _blogService.GetBlogPostsAsync();
            var model = result.Where(_ => _.GetByLangWithDefault().CreateTime.Year == year).ToList();

            var pagination = new Pagination(p, (int)Math.Ceiling(result.Count / (double)_config.PaginationChunkSize));
            var posts = model.ToPagedObjects(_config.PaginationChunkSize, pagination.CurrentPage);

            if (pagination.CurrentPage > 1)
            {
                title += $" (Page {pagination.CurrentPage})";
                path += $"?p={pagination.CurrentPage}";
            }

            ViewData["Title"] = title;
            ViewData["Path"] = path;
            return View("Detail", new PagedBlogPosts { Pagination = pagination, Posts = posts, Url = Request.Path });
        }
    }
}