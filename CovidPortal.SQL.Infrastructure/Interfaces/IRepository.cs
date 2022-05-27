using CovidPortal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CovidPortal.SQL.Infrastructure.Interfaces
{
    public interface IRepository<T> : IRepository<T, string> where T : IEntityBase
    {
    }

    public interface IRepository<T, TKey> where T : IEntityBase
    {
        Task AddRange(params T[] entities);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task UpdateRange(params T[] entity);

        Task Remove(T entity);

        Task RemoveRange(params T[] entities);

        Task<IQueryable<T>> Queryable();

        Task<T> GetEntity(Expression<Func<T, bool>> filter);

        Task<T> GetEntityById(TKey key);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
