using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using MvcConnerstore.Cache.Repositories;

namespace MvcConnerstore.Cache.Persistance
{
    public class MemoryCacheRepository<T> : ICacheRepository<T> where T : class
    {
        private readonly MemoryCache memoryCache = new MemoryCache("MicroCMS.Cache");
        static readonly object lockObj = new object();
        
        public void Add(string key, T item)
        {
            lock (lockObj)
            {
                memoryCache.Add(key, item, DateTimeOffset.MaxValue);
            }
        }

        public void Add(string key, IEnumerable<T> items)
        {
            lock (lockObj)
            {
                memoryCache.Add(key, items, DateTimeOffset.MaxValue);
            }
        }

        public void Add(string key, object item)
        {
            lock (lockObj)
            {
                memoryCache.Add(key, item, DateTimeOffset.MaxValue);
            }
        }

        public void Add(string key, T item, int timeStamp)
        {
            throw new NotImplementedException();
        }

        public T Gets(string key)
        {
            lock (lockObj)
            {
                return (T)memoryCache[key];
            }
        }
    }
}
