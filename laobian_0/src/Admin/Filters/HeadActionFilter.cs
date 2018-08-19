using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Mvc.Head;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Laobian.Admin.Filters
{
    public class HeadActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headItem = new HeadItem();
            headItem.Icon = "/cat.png";

            context.HttpContext.Items[HeadItem.HeadItemKey] = headItem;
            await next();
        }
    }
}