using Laobian.Infrastuture.Auth;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Repository;
using Laobian.Service.Base;
using Laobian.Service.Component;
using Laobian.Service.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Laobian.Service.Helper
{
    public class Bootstrapper
    {
        public static void InitAdmin(IServiceCollection services)
        {
            InitCommon(services);
            services.AddSingleton<IBlogPostRepository, BlogPostRepository>();
            services.AddSingleton<IBlogService, BlogService>();
            services.AddSingleton<ILog4BlogVisitRepository, Log4BlogVisitRepository>();
            services.AddSingleton<AdminAuthFilter>();
        }

        public static void InitBlog(IServiceCollection services)
        {
            InitCommon(services);
            services.AddSingleton<IBlogPostRepository, BlogPostRepository>();
            services.AddSingleton<IBlogService, BlogService>();
            services.AddSingleton<ILog4BlogVisitRepository, Log4BlogVisitRepository>();
            services.AddSingleton<ILog4BlogVisitTask, Log4BlogVisitTask>();
        }

        public static void InitApi(IServiceCollection services)
        {
            InitCommon(services);
        }

        public static void InitHome(IServiceCollection services)
        {
            InitCommon(services);
            services.AddSingleton<AdminAuthFilter>();
        }

        private static void InitCommon(IServiceCollection services)
        {
            services.AddSingleton<ILaobianCache, LaobianCache>();
            services.AddSingleton<ILaobianEmailSender, LaobianEmailSender>();
            services.AddSingleton<ILaobianAzureStorage, LaobianAzureStorage>();
            services.AddSingleton<ILog4CommonRepository, Log4CommonRepository>();
            services.AddSingleton<ILog4CommonTask, Log4CommonTask>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserPermissionRepository, UserPermissionRepository>();
            services.AddSingleton<IUserService, UserService>();
        }
    }
}
