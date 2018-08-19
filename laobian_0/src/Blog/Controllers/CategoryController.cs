using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Blog.Models;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Mvc.Head;
using Laobian.Share.Model;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;

        public CategoryController(IBlogService blogService, ISettingService settingSerive)
        {
            _blogService = blogService;
            _settingService = settingSerive;
        }

        [HttpGet]
        [Route("/category")]
        [ResponseCache(Duration = 60 * 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.FindAllPostAsync(true);
            var cats = await _blogService.FindAllCategoryAsync();
            var model = new BlogCategoryViewModel(posts.OrderByDescending(_ => _.CreateAt).ToList(), cats.ToList());
            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Category";
            headItem.Canonical = "/category";
            headItem.Description = $"Category - {await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)}";

            return View("Index", model);
        }

        [HttpGet]
        [Route("/category/{cat}")]
        [ResponseCache(Duration = 60 * 60 * 12, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Detail([FromRoute] string cat)
        {
            var posts = new List<BlogPost>(await _blogService.FindAllByCategoryAsync(cat, true));
            var cats = new List<BlogCategory>(await _blogService.FindAllCategoryAsync());
            var item = cats.FirstOrDefault(c => string.Equals(cat, c.Url, StringComparison.InvariantCultureIgnoreCase));
            if (item == null)
            {
                return NotFound();
            }

            var model = new BlogCategoryViewModel(posts.OrderByDescending(_ => _.CreateAt).ToList(), cats) {SelecteCategory = item.Id};
            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = $"{item.Name} · Category";
            headItem.Robots = "noindex,nofollow";
            headItem.Description = $"Category for {item.Name} - {await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)}";


            var randomPostItem = (RandomPostFactor) HttpContext.Items[RandomPostFactor.ItemKey];
            randomPostItem.ExcludePosts = posts.Select(_ => _.Id);
            return View("Index", model);
        }
    }
}