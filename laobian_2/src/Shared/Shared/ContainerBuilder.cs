using Laobian.Shared.Blog;
using Laobian.Shared.Interface;
using Laobian.Shared.Interface.Blog;
using Laobian.Shared.Interface.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laobian.Shared
{
    public static class ContainerBuilder
    {
        public static void RegisterAdmin(IServiceCollection services, IConfiguration configuration)
        {
            RegisterCommon(services, configuration);

            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<IBlogService, BlogService>();
            services.Configure<AdminConfiguration>(configuration);
        }

        public static void RegisterBlog(IServiceCollection services, IConfiguration configuration)
        {
            RegisterCommon(services, configuration);

            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<IBlogService, BlogService>();
            services.Configure<BlogConfiguration>(configuration);
        }

        private static void RegisterCommon(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISharedRepository, SharedRepository>();
            services.AddSingleton<ISharedService, SharedService>();
            services.Configure<ConfigurationBase>(configuration);
        }
    }
}
