using System.Threading.Tasks;

namespace Laobian.Share.Domain.Interface
{
    public interface IBlogPostService
    {
        Task AddVisitAsync(int year, int month, string url);
    }
}