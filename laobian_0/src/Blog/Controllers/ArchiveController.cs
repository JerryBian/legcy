using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Blog.Models;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Mvc.Head;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;

        public ArchiveController(IBlogService blogService, ISettingService settingService)
        {
            _blogService = blogService;
            _settingService = settingService;
        }

        [HttpGet]
        [Route("/archive")]
        [ResponseCache(Duration = 60 * 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.FindAllPostAsync(true);
            var dates = new List<DateTimeViewModel>();
            foreach (var post in posts.GroupBy(_ => new DateTime(_.CreateAt.Year, 1, 1)).OrderByDescending(_ => _.Key))
                dates.Add(new DateTimeViewModel($"{post.Key.Year} 年", $"/archive/{post.Key.Year}"));

            var model = new BlogArchiveViewModel(posts.OrderByDescending(_ => _.CreateAt).ToList(), dates);
            var headItem = (HeadItem)HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Archive";
            headItem.Canonical = "/archive";
            headItem.Description = $"Archive - {await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)}";

            return View("Index", model);
        }

        [HttpGet]
        [Route("/archive/{year:int}")]
        [ResponseCache(Duration = 60 * 60 * 12, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetPostsByYear([FromRoute] int year)
        {
            var posts = await _blogService.FindAllPostAsync(true);
            var dates = new List<DateTimeViewModel>();
            foreach (var post in posts.GroupBy(_ => new DateTime(_.CreateAt.Year, 1, 1)).OrderByDescending(_ => _.Key))
                dates.Add(new DateTimeViewModel($"{post.Key:yyyy 年}", $"/archive/{post.Key.Year}"));

            posts = posts.Where(_ => _.CreateAt.Year == year).ToList();
            var model = new BlogArchiveViewModel(posts.OrderByDescending(_ => _.CreateAt).ToList(), dates)
            {
                SelectedDate = $"/archive/{year}"
            };

            var headItem = (HeadItem)HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = $"{year}年 · Archive";
            headItem.Robots = "noindex,nofollow";
            headItem.Description = $"Archive for {year} 年 - {await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)}";

            var randomPostItem = (RandomPostFactor)HttpContext.Items[RandomPostFactor.ItemKey];
            randomPostItem.ExcludePosts = posts.Select(_ => _.Id);

            return View("Index", model);
        }

        [HttpGet]
        [Route("/archive/{year:int}/{month:int}")]
        [ResponseCache(Duration = 60 * 60 * 12, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetPostsByYearMonth([FromRoute] int year, [FromRoute] int month)
        {
            var posts = await _blogService.FindAllPostAsync(true);
            var dates = new List<DateTimeViewModel>();
            foreach (var post in posts.GroupBy(_ => new DateTime(_.CreateAt.Year, _.CreateAt.Month, 1))
                .OrderByDescending(_ => _.Key))
                dates.Add(new DateTimeViewModel($"{post.Key:yyyy 年 MM 月}",
                    $"/archive/{post.Key.Year}/{post.Key.Month:00}"));

            posts = posts.Where(_ => _.CreateAt.Year == year && _.CreateAt.Month == month).ToList();

            var model = new BlogArchiveViewModel(posts.OrderByDescending(_ => _.CreateAt).ToList(), dates)
            {
                SelectedDate = $"/archive/{year}/{month:00}"
            };

            var headItem = (HeadItem)HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = $"{year}年{month:00}月 · Archive";
            headItem.Robots = "noindex,nofollow";
            headItem.Description = $"Archive for {year} 年 {month:00} 月 - {await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)}";


            var randomPostItem = (RandomPostFactor)HttpContext.Items[RandomPostFactor.ItemKey];
            randomPostItem.ExcludePosts = posts.Select(_ => _.Id);
            return View("Index", model);
        }
    }
}