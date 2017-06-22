using Laobian.Infrastuture.Model;
using System.Threading.Tasks;

namespace Laobian.Infrastuture.Interface.Service
{
    public interface ILaobianEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
