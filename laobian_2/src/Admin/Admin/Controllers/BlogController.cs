using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Shared.Interface.Blog;
using Laobian.Shared.Interface.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Admin.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService, IOptions<AdminConfiguration> config)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Posts()
        {
            var result = await _blogService.GetBlogPostsAsync(false);
            return View(result);
        }

        [HttpGet]
        [Route("/blog/post/add")]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [Route("/blog/post/add")]
        public async Task<IActionResult> AddPost([FromBody]Laobian.Admin.Models.BlogPost post)
        {
            var model = new BlogPost();
            model.Url = post.Url;
            model.AllowComment = post.AllowComment;

            if (!string.IsNullOrEmpty(post.MdEn) &&
                !string.IsNullOrEmpty(post.TitleEn))
            {
                model.Contents.Add(new BlogPostContent
                {
                    IsEnglish = true,
                    IsPublic = post.IsPublicEn,
                    Markdown = post.MdEn,
                    Title = post.TitleEn
                });
            }

            if (!string.IsNullOrEmpty(post.MdZh) &&
                !string.IsNullOrEmpty(post.TitleZh))
            {
                model.Contents.Add(new BlogPostContent
                {
                    IsEnglish = false,
                    IsPublic = post.IsPublicZh,
                    Markdown = post.MdZh,
                    Title = post.TitleZh
                });
            }



            model.TagsString = post.Tags;

            if (model.Tags.Any(_ => !BlogTag.CheckName(_.Name)))
            {
                return BadRequest("The tags contains invalid chars.");
            }

            var result = await _blogService.AddBlogPostAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("/blog/post/update")]
        public async Task<IActionResult> UpdatePost([FromQuery] int id)
        {
            var result = await _blogService.GetBlogPostByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        [Route("/blog/post/update")]
        public async Task<IActionResult> UpdatePost([FromBody]Laobian.Admin.Models.BlogPost post)
        {
            var model = new BlogPost();
            model.Id = post.Id;
            model.Url = post.Url;
            model.AllowComment = post.AllowComment;

            if (!string.IsNullOrEmpty(post.MdEn) &&
                !string.IsNullOrEmpty(post.TitleEn))
            {
                model.Contents.Add(new BlogPostContent
                {
                    IsEnglish = true,
                    IsPublic = post.IsPublicEn,
                    Markdown = post.MdEn,
                    Title = post.TitleEn
                });
            }

            if (!string.IsNullOrEmpty(post.MdZh) &&
                !string.IsNullOrEmpty(post.TitleZh))
            {
                model.Contents.Add(new BlogPostContent
                {
                    IsEnglish = false,
                    IsPublic = post.IsPublicZh,
                    Markdown = post.MdZh,
                    Title = post.TitleZh
                });
            }

            model.TagsString = post.Tags;

            await _blogService.UpdateBlogPostAsync(model);
            return Ok();
        }
    }
}