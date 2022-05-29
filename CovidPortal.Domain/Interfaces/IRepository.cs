using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CovidPortal.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntityBase
    {
        Task AddRange(params T[] entities);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task UpdateRange(params T[] entity);

        Task Remove(T entity);

        Task RemoveRange(params T[] entities);

        Task<IQueryable<T>> Queryable();

        Task<T> GetEntity(Expression<Func<T, bool>> filter);

        Task<T> GetEntityById(string key);

        Task<IEnumerable<T>> GetAllAsync();

        Task<bool> HasDataAsync(Expression<Func<T, bool>> predicate);
    }
}
