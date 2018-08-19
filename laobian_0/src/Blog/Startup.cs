using Laobian.Blog.Filters;
using Laobian.Share.Domain;
using Laobian.Share.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laobian.Blog
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
            StartupHelper.ConfigureServices(services, Configuration);
            services.AddResponseCaching();
            services.AddMvc(options => { options.Filters.Add(typeof(BlogActionFilter)); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            StartupHelper.Config(app, env, applicationLifetime, DomainName.Blog);

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}