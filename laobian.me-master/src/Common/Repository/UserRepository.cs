using Laobian.Infrasture.Entity.User;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Laobian.Infrastuture.Const;
using System.Threading.Tasks;
using Laobian.Infrastuture.Model;

namespace Laobian.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IOptions<AppSettings> setting) : base(setting)
        {
            var user = SelectAsync(_ => _.Role == UserRole.Admin.ToString()).Result;
            if (!user.Any())
            {
                AddAsync(new User {
                    FullName = setting.Value.AdminFullName,
                    UserName = setting.Value.AdminUserName,
                    Role = UserRole.Admin.ToString(),
                    Password = setting.Value.AdminPassword,
                    Email = setting.Value.AdminEmail
                }).Wait();
            }
        }

        protected override IQueryable<User> Include(DbSet<User> dbSet)
        {
            return dbSet.Include(_ => _.UserPermission);
        }

        public override Task AddAsync(User entity)
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            return base.AddAsync(entity);
        }
    }
}
