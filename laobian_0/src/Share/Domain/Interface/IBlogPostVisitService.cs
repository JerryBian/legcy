using System;
using System.Threading.Tasks;

namespace Laobian.Share.Domain.Interface
{
    public interface IBlogPostVisitService
    {
        Task AddAsync(Guid postId);

        Task GetOrResetAsync();
    }
}