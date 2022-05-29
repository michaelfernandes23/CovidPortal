using CovidPortal.Domain.Interfaces;
using CovidPortal.SQL.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CovidPortal.SQL.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntityBase
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        protected virtual DbSet<T> DbSet => _dbSet;

        protected virtual DbContext DbContext => _dbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual Task AddRange(params T[] entity)
        {
            _dbSet.AddRange(entity);
            return Task.CompletedTask;
        }

        public virtual Task<T> Add(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            return Task.FromResult(_dbSet.Add(entity).Entity);
        }

        public virtual Task<T> Update(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
            }
            catch
            {
            }

            return Task.FromResult(_dbSet.Update(entity).Entity);
        }

        public virtual Task UpdateRange(params T[] entity)
        {
            _dbSet.UpdateRange(entity);
            return Task.CompletedTask;
        }

        public virtual Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveRange(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task<IQueryable<T>> Queryable()
        {
            return Task.FromResult(_dbSet.AsQueryable());
        }

        public virtual Task<T> GetEntity(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefaultAsync(filter);
        }

        public Task<T> GetEntityById(string key)
        {
            return _dbContext.FindAsync<T>(new object[1]
            {
                key
            }).AsTask();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_dbContext.Set<T>().AsEnumerable());
        }
    }
}
