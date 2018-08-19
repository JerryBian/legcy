using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Blog.Models;
using Laobian.Share.Data.Repository;
using Laobian.Share.Model.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public HomeController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]int p)
        {
            var posts = await _blogRepository.FindAllPostsAsync();
            var pagination = new Pagination(p, (int)Math.Ceiling(posts.Count() / (double)2));
            var pagedPosts = posts.ToPagedObjects(2, pagination.CurrentPage);
            var model = new PagedPostViewModel
            {
                Pagination = pagination,
                Posts = pagedPosts,
                Url = Request.Path
            };
            return View(model);
        }

        [HttpGet]
        [Route("/{year:int}/{month:int}/{url}.html")]
        public async Task<IActionResult> Post([FromRoute]int year, [FromRoute]int month, [FromRoute]string url)
        {
            var post = await _blogRepository.FindPostByUrlAsync(year, month, url);
            return View(post);
        }

        [HttpGet]
        [Route("/category")]
        public async Task<IActionResult> Category()
        {
            var posts = await _blogRepository.FindAllPostsAsync();
            var categories = await _blogRepository.FindAllCategooriesAsync();
            var model = new CategoryViewModel(posts, categories);
            return View(model);
        }

        [HttpGet]
        [Route("/category/{url}")]
        public async Task<IActionResult> Category([FromRoute]string url)
        {
            var posts = await _blogRepository.FindPostsByCategoryAsync(url);
            var categories = await _blogRepository.FindAllCategooriesAsync();
            var model = new CategoryViewModel(posts, categories) { SelecteCategory = posts.First().Category.Id };
            return View(model);
        }

        [HttpGet]
        [Route("/archive")]
        public async Task<IActionResult> Archive()
        {
            var posts = await _blogRepository.FindAllPostsAsync();
            var dates = new List<DateViewModel>();
            foreach (var item in posts.GroupBy(p => new DateTime(p.CreateTime.Year, 1, 1)).OrderByDescending(p => p.Key))
            {
                dates.Add(new DateViewModel($"{item.Key:yyyy 年}", $"/archive/{item.Key.Year}"));
            }
            var model = new ArchiveViewModel(posts, dates);
            return View(model);
        }

        [HttpGet]
        [Route("/archive/{year}")]
        public async Task<IActionResult> Archive([FromRoute]int year)
        {
            var allPosts = await _blogRepository.FindAllPostsAsync();
            var posts = await _blogRepository.FindPostsByDateAsync(year);
            var dates = new List<DateViewModel>();
            foreach (var item in allPosts.GroupBy(p => new DateTime(p.CreateTime.Year, 1, 1)).OrderByDescending(p => p.Key))
            {
                dates.Add(new DateViewModel($"{item.Key:yyyy 年}", $"/archive/{item.Key.Year}"));
            }
            var model = new ArchiveViewModel(posts, dates)
            {
                SelectedDate = $"/archive/{year}"
            };

            return View(model);
        }

        [HttpGet]
        [Route("/archive/{year}/{month}")]
        public async Task<IActionResult> Archive([FromRoute]int year, [FromRoute]int month)
        {
            var allPosts = await _blogRepository.FindAllPostsAsync();
            var posts = await _blogRepository.FindPostsByDateAsync(year, month);
            var dates = new List<DateViewModel>();
            foreach (var item in allPosts.GroupBy(p => new DateTime(p.CreateTime.Year, 1, 1)).OrderByDescending(p => p.Key))
            {
                dates.Add(new DateViewModel($"{item.Key:yyyy 年}", $"/archive/{item.Key.Year}"));
            }

            var model = new ArchiveViewModel(posts, dates);

            return View(model);
        }
    }
}
