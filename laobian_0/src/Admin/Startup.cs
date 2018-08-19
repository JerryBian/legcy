using System;
using Laobian.Admin.Filters;
using Laobian.Share.Domain;
using Laobian.Share.Infrastructure;
using Laobian.Share.Infrastructure.Email;
using Laobian.Share.Infrastructure.Mvc;
using Laobian.Share.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laobian.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<AuthenticationEvents>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "laobian";
                    options.Cookie.Path = "/";
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.LoginPath = "/identity/login";
                    options.LogoutPath = "/identity/logout";
                    options.ReturnUrlParameter = "r";
                    options.EventsType = typeof(AuthenticationEvents);
                });

            StartupHelper.ConfigureServices(services, Configuration);
            services.AddMvc(options => options.Filters.Add(typeof(HeadActionFilter)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            StartupHelper.Config(app, env, applicationLifetime, DomainName.Admin);

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}