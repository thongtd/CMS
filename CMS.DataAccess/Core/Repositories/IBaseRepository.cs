using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get<T>(T Id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
