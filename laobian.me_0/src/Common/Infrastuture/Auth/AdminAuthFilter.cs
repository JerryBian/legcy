using Laobian.Infrastuture.Const;
using Laobian.Infrastuture.Extension;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Model;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Laobian.Infrastuture.Auth
{
    public class AdminAuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IUserService _userService;
        private readonly AppSettings _settings;

        public AdminAuthFilter(
            IOptions<AppSettings> settings,
            IUserService userService)
        {
            _userService = userService;
            _settings = settings.Value;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var baseUrl = _settings.HomeHost;
            var user = context.HttpContext.Request.ExtractUser(_settings.Salt);
            if (user == null)
            {
                context.HttpContext.Response.SignOut(baseUrl, context.HttpContext.Request.GetEncodedUrl());
            }
            else
            {
                var entity = await _userService.FindAsync(user.Id);
                context.HttpContext.Items["user"] = entity;
                if (entity == null || entity.Role != UserRole.Admin.ToString() || entity.UpdateTime != user.UpdateTime)
                {
                    context.HttpContext.Response.SignOut(baseUrl, context.HttpContext.Request.GetEncodedUrl());
                }
            }
        }
    }
}
