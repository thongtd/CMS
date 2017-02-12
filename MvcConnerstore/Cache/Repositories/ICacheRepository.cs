using System.Collections.Generic;

namespace MvcConnerstore.Cache.Repositories
{
    public interface ICacheRepository<T> where T : class
    {
        void Add(string key, T item);

        void Add(string key, IEnumerable<T> items);

        void Add(string key, object item);

        void Add(string key, T item, int timeStamp);

        T Gets(string key);
    }
}
