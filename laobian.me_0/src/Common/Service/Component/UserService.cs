using Laobian.Infrasture.Entity.User;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Interface.Service;
using System.Threading.Tasks;

namespace Laobian.Service.Component
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserService(IUserRepository userRepository, IUserPermissionRepository userPermissionRepository)
        {
            _userRepository = userRepository;
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<User> FindAsync(int id)
        {
            return await _userRepository.FindAsync(id);
        }

        public async Task<User> ValidateUserAsync(string userNameOrEmail, string password)
        {
            var result = await GetUserByUserNameAsync(userNameOrEmail, password);
            if(result == null)
            {
                result = await GetUserByEmailAsync(userNameOrEmail, password);
            }

            return result;
        }

        public async Task<User> GetUserByUserNameAsync(string userName, string password)
        {
            var record = await _userRepository.SingleAsync(_ => _.UserName == userName);
            if (record == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, record.Password))
            {
                return null;
            }

            return record;
        }

        public async Task<User> GetUserByEmailAsync(string email, string password)
        {
            var record = await _userRepository.SingleAsync(_ => _.Email == email);
            if (record == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, record.Password))
            {
                return null;
            }

            return record;
        }
    }
}
