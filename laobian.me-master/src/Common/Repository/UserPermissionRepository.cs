using Laobian.Infrasture.Entity.User;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;
using Microsoft.Extensions.Options;

namespace Laobian.Repository
{
    public class UserPermissionRepository : RepositoryBase<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(IOptions<AppSettings> setting) : base(setting)
        {
        }
    }
}
