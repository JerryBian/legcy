using Laobian.Infrasture.Entity.User;
using System.Threading.Tasks;

namespace Laobian.Infrastuture.Interface.Service
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string userNameOrEmail, string password);

        Task<User> FindAsync(int id);
    }
}
