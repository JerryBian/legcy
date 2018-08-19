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
    public class TagController : Controller
    {
        private readonly BlogConfiguration _config;
        private readonly IBlogService _blogService;

        public TagController(IBlogService blogService, IOptions<BlogConfiguration> config)
        {
            _config = config.Value;
            _blogService = blogService;
        }

        [HttpGet]
        [Route("/tags")]
        public async Task<IActionResult> Index()
        {
            var result = await _blogService.GetBlogPostsAsync();
            var model = new List<TagsViewModel>();
            var tags = result.SelectMany(_ => _.Tags).Select(t => t.Name).Distinct();
            foreach (var tag in tags)
            {
                var posts = result.Where(_ => _.Tags.Select(t => t.Name).Contains(tag)).ToList();
                model.Add(new TagsViewModel
                {
                    Count = posts.Count(),
                    Name = tag,
                    Posts = posts
                });
            }
            return View(model);
        }

        [HttpGet]
        [Route("/tag/{tag}")]
        public async Task<IActionResult> GetPostsByTag([FromRoute]string tag, [FromQuery] int p)
        {
            ViewData["tag"] = tag;
            var result = await _blogService.GetBlogPostsByTagAsync(tag);
            var pagination = new Pagination(p, (int)Math.Ceiling(result.Count / (double)_config.PaginationChunkSize));
            var posts = result.ToPagedObjects(_config.PaginationChunkSize, pagination.CurrentPage);
            return View("Detail", new PagedBlogPosts { Pagination = pagination, Posts = posts, Url = Request.Path });
        }
    }
}