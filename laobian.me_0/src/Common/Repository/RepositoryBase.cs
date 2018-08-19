using Laobian.Infrasture.Context;
using Laobian.Infrastuture.Entity;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Laobian.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly AppSettings _setting;

        public RepositoryBase(IOptions<AppSettings> setting)
        {
            _setting = setting.Value;
            using (var context = CreateDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        protected virtual MySqlContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<MySqlContext>();
            builder.UseMySql(_setting.MySqlConnectionString);
            var context = new MySqlContext(builder.Options);
            context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(3).TotalMilliseconds);
            return context;
        }

        public virtual async Task AddAsync(T entity)
        {
            if (entity.CreateTime == default(DateTime))
            {
                entity.CreateTime = DateTime.UtcNow;
            }

            if (entity.UpdateTime == default(DateTime))
            {
                entity.UpdateTime = DateTime.UtcNow;
            }

            using (var context = CreateDbContext())
            {
                context.Set<T>().Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public virtual async Task AddAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreateTime == default(DateTime))
                {
                    entity.CreateTime = DateTime.UtcNow;
                }

                if (entity.UpdateTime == default(DateTime))
                {
                    entity.UpdateTime = DateTime.UtcNow;
                }
            }

            using (var context = CreateDbContext())
            {
                context.Set<T>().AddRange(entities);

                await context.SaveChangesAsync();
            }
        }

        public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            using (var context = CreateDbContext())
            {
                return await Include(context.Set<T>()).LongCountAsync(predicate);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            using (var context = CreateDbContext())
            {
                var item = await context.Set<T>().FirstOrDefaultAsync(_ => _.Id == id);
                if (item != null)
                {
                    context.Set<T>().Remove(item);
                    await context.SaveChangesAsync();
                }
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var item = await FindAsync(entity.Id);
            if (item != null)
            {
                await DeleteAsync(entity.Id);
                entity.CreateTime = item.CreateTime;
                entity.UpdateTime = DateTime.UtcNow;
                await AddAsync(entity);
            }
        }

        public virtual async Task<T> FindAsync(int id)
        {
            using (var context = CreateDbContext())
            {
                return await Include(context.Set<T>()).FirstOrDefaultAsync(_ => _.Id == id);
            }
        }

        public virtual async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            using (var context = CreateDbContext())
            {
                return await Include(context.Set<T>()).Where(predicate).SingleOrDefaultAsync();
            }
        }

        public virtual async Task<List<T>> SelectAsync(
            Expression<Func<T, bool>> predicate)
        {
            using (var context = CreateDbContext())
            {
                return await Include(context.Set<T>()).Where(predicate).ToListAsync();
            }
        }

        protected virtual IQueryable<T> Include(DbSet<T> dbSet)
        {
            return dbSet.AsQueryable();
        }
    }
}
