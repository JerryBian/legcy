using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Domain.Blog;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Model;

namespace Laobian.Share.Domain.Interface
{
    /// <summary>
    /// Generator of the random posts widget
    /// </summary>
    public interface IRandomPostGenerator
    {
        /// <summary>
        /// Exectue the request
        /// </summary>
        /// <param name="factor">Parameters of the request</param>
        /// <returns>A list of <see cref="BlogPost"/></returns>
        /// <remarks>The result is cached according to the input factor.</remarks>
        Task<List<BlogPost>> Execute(RandomPostFactor factor);
    }
}