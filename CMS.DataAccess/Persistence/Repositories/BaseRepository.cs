using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        protected BaseRepository(DbContext context)
        {
            this.Context = context;
        }
        
        public TEntity Get<T>(T Id)
        {
            return Context.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return Context.Set<TEntity>().ToList();
            }
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAsyn(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return await Context.Set<TEntity>().ToListAsync();
            }
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
