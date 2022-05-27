using CovidPortal.Domain.Interfaces;
using CovidPortal.SQL.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CovidPortal.SQL.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : RepositoryBase<T, string>, IRepository<T>, IRepository<T, string> where T : class, IEntityBase
    {
        protected RepositoryBase(DbContext dbContext, IDbConnection dbConnection, string tableName)
            : base(dbContext, dbConnection, tableName)
        {
        }

        protected RepositoryBase(DbContext dbContext, IDbConnection dbConnection)
            : base(dbContext, dbConnection)
        {
        }

        public override Task<T> Add(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            return Task.FromResult(DbSet.Add(entity).Entity);
        }
    }

    public abstract class RepositoryBase<T, TKey> : IRepository<T, TKey> where T : class, IEntityBase
    {
        private readonly DbContext _dbContext;

        private readonly IDbConnection _dbConnection;

        private readonly string _tableName;

        private readonly DbSet<T> _dbSet;

        protected virtual DbSet<T> DbSet => _dbSet;

        protected virtual DbContext DbContext => _dbContext;

        protected virtual IDbConnection DbConnection => _dbConnection;

        protected virtual string TableName => _tableName;

        protected RepositoryBase(DbContext dbContext, IDbConnection dbConnection, string tableName)
        {
            _dbContext = dbContext;
            _dbConnection = dbConnection;
            _tableName = tableName;
            _dbSet = _dbContext.Set<T>();
        }

        protected RepositoryBase(DbContext dbContext, IDbConnection dbConnection)
            : this(dbContext, dbConnection, (string)null)
        {
            TableAttribute customAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
            if (customAttribute == null)
            {
                _tableName = typeof(T).Name;
                return;
            }

            _tableName = ((!string.IsNullOrEmpty(customAttribute.Schema)) ? ("[" + customAttribute.Schema + "].[" + customAttribute.Name + "]") : customAttribute.Name);
        }

        public virtual Task AddRange(params T[] entity)
        {
            _dbSet.AddRange(entity);
            return Task.CompletedTask;
        }

        public virtual Task<T> Add(T entity)
        {
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

        public Task<T> GetEntityById(TKey key)
        {
            return _dbContext.FindAsync<T>(new object[1]
            {
                key
            }).AsTask();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return _dbConnection.QueryAsync<T>("SELECT * FROM " + _tableName);
        }
    }
}
