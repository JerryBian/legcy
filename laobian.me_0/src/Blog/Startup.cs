using Laobian.Infrastuture.Const;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Middleware;
using Laobian.Infrastuture.Model;
using Laobian.Service.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Laobian.Blog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs }));
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            Bootstrapper.InitBlog(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ILog4CommonTask log4CommonTask)
        {
            loggerFactory.AddConsole(Microsoft.Extensions.Logging.LogLevel.Error);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
            }

            ExceptionMiddleware.Use(app, log4CommonTask, HostComponent.Blog);
            ForwardMiddleware.Use(app);
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
