using Jarvis.Strategy;
using Laobian.Share.Domain.Blog;
using Laobian.Share.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Jarvis.Job
{
    public class BlogPostVisitJob : JobBase
    {
        public BlogPostVisitJob(ServiceProvider serviceProvider) : base(serviceProvider)
        {
            var blogPostVisitService = serviceProvider.GetService<IBlogPostVisitService>();
            Strategy = new RunEverySecondJobStrategy(60 * 60);
            JobAction = async () => { await blogPostVisitService.GetOrResetAsync(); };
        }
    }
}