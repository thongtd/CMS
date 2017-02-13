using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using MvcConnerstore.Cache.Repositories;

namespace MvcConnerstore.Cache.Persistance
{
    public class MemoryCacheRepository<T> : ICacheRepository<T> where T : class
    {
        private static MemoryCacheRepository<T> instance = null;
        //private readonly ObjectCache memoryCache = null;
        static readonly object padlock = new object();

        public MemoryCacheRepository()
        {
            
        }

        public void Add(string key, T item)
        {
            lock (padlock)
            {
                MemoryCache memoryCache = MemoryCache.Default;
                memoryCache.Add(key, item, DateTimeOffset.MaxValue);
            }
        }

        public void Add(string key, IEnumerable<T> items)
        {
            lock (padlock)
            {
                MemoryCache memoryCache = MemoryCache.Default;
                memoryCache.Add(key, items, DateTimeOffset.MaxValue);
            }
        }

        public void Add(string key, object item)
        {
            lock (padlock)
            {
                MemoryCache memoryCache = MemoryCache.Default;
                memoryCache.Add(key, item, DateTimeOffset.MaxValue);
            }
        }

        public void Add(string key, T item, int timeStamp)
        {
            throw new NotImplementedException();
        }

        public T Gets(string key)
        {
            lock (padlock)
            {
                MemoryCache memoryCache = MemoryCache.Default;
                return (T)memoryCache[key];
            }
        }
    }
}
