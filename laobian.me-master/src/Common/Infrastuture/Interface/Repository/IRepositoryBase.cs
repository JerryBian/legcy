using Laobian.Infrastuture.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Laobian.Infrastuture.Interface.Repository
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task AddAsync(T entity);

        Task AddAsync(IEnumerable<T> entities);

        Task<long> CountAsync(Expression<Func<T, bool>> predicate);

        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);

        Task<T> FindAsync(int id);

        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> SelectAsync(Expression<Func<T, bool>> predicate);
    }
}
