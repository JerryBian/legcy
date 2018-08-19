using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Email;

namespace Laobian.Share.Infrastructure.Interface
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message);
    }
}