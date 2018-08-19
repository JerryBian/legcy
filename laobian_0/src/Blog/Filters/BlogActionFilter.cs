using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Mvc.Head;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Laobian.Blog.Filters
{
    public class BlogActionFilter : IAsyncActionFilter
    {
        private readonly ISettingService _settingService;

        public BlogActionFilter(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headItem = new HeadItem
            {
                BaseTitle = await _settingService.FindAsync<string>(SettingKey.BlogDefaultTitle),
                Robots = "index,follow,archive",
                Icon = "/cat.png", //https://www.flaticon.com/free-icon/cat_826980#term=cat&page=1&position=7
                Alternate = "/rss",
                BaseUrl = $"https://{context.HttpContext.Request.Host.Value}",
                Author = await _settingService.FindAsync<string>(SettingKey.Author),
                DnsPrefetch = new List<string> { "//laobian.blob.core.windows.net", "//www.googletagmanager.com", "//cdnjs.cloudflare.com", "//www.google-analytics.com" }
            };

            context.HttpContext.Items[HeadItem.HeadItemKey] = headItem;
            context.HttpContext.Items[RandomPostFactor.ItemKey] = new RandomPostFactor();
            await next();
        }
    }
}