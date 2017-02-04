using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get<T>(T Id);

        Task<TEntity> GetAsyn<T>(T Id);

        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsyn(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
