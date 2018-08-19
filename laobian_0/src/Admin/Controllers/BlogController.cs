using System;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Admin.Models;
using Laobian.Share.Domain.Blog;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Infrastructure.Identity;
using Laobian.Share.Infrastructure.Mvc.Head;
using Laobian.Share.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Authorize]
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> Post()
        {
            var posts = await _blogService.FindAllPostAsync();
            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Post Management · Blog";
            return View(posts.OrderByDescending(_ => _.CreateAt));
        }

        [HttpGet]
        [Route("post/new")]
        public async Task<IActionResult> NewPost()
        {
            var categories = await _blogService.FindAllCategoryAsync();
            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "New Post · Blog";
            return View(categories);
        }

        [HttpPost]
        [Route("post/new")]
        public async Task<IActionResult> NewPost([FromBody] BlogNewPostViewModel blogPostViewModel)
        {
            var user = (User) HttpContext.Items[IdentityConstant.ItemKey];

            await _blogService.AddPostAsync(blogPostViewModel.ToBlogPost());
            return Ok();
        }

        [HttpGet]
        [Route("post/update/{postId:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid postId)
        {
            var post = await _blogService.FindPostAsync(postId);
            var categories = await _blogService.FindAllCategoryAsync();
            var model = new BlogUpdatePostViewModel
            {
                AllCategories = categories,
                CreateAt = post.CreateAt,
                Html = post.Html,
                Id = post.Id,
                IsPublish = post.IsPublish,
                LastUpdateAt = post.LastUpdateAt,
                Markdown = post.Markdown,
                Title = post.Title,
                Url = post.Url,
                IsPrivate = post.IsPrivate
            };
            model.BlogCategories.AddRange(post.BlogCategories);

            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Update Post · Blog";
            return View(model);
        }

        [HttpPost]
        [Route("post/update")]
        public async Task<IActionResult> UpdatePost([FromBody] BlogUpdatePostViewModel blogUpdatePostViewModel)
        {
            var user = (User) HttpContext.Items[IdentityConstant.ItemKey];
            blogUpdatePostViewModel.LastUpdateAt = DateTime.UtcNow;

            await _blogService.UpdatePostAsync(blogUpdatePostViewModel.ToBlogPost());
            return Ok();
        }

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> Category()
        {
            var categories = await _blogService.FindAllCategoryAsync();

            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Category Management · Blog";
            return View(categories.OrderBy(_ => _.Name));
        }

        [HttpGet]
        [Route("category/new")]
        public IActionResult NewCategory()
        {
            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "New Category · Blog";
            return View();
        }

        [HttpPost]
        [Route("category/new")]
        public async Task<IActionResult> NewCategory([FromBody] BlogCategory blogCategory)
        {
            var user = (User) HttpContext.Items[IdentityConstant.ItemKey];
            blogCategory.LastUpdateAt = DateTime.UtcNow;
            await _blogService.AddCategoryAsync(blogCategory);
            return Ok();
        }

        [HttpGet]
        [Route("category/update/{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id)
        {
            var category = await _blogService.FindCategoryAsync(id);
            if (category == null) return NotFound();

            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Update Category · Blog";
            return View(category);
        }

        [HttpPost]
        [Route("category/update")]
        public async Task<IActionResult> UpdateCategory([FromBody] BlogCategory blogCategoryViewModel)
        {
            var user = (User) HttpContext.Items[IdentityConstant.ItemKey];
            blogCategoryViewModel.LastUpdateAt = DateTime.UtcNow;
            await _blogService.UpdateCategoryAsync(blogCategoryViewModel);
            return Ok();
        }
    }
}