using Repo.Int;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Imp
{
    public class MemoryCache : ICache
    {

        private System.Runtime.Caching.MemoryCache cache;

        public int ItemLifeMinutes
        {
            get
            {

                DateTime fromDate = DateTime.UtcNow.AddDays(1).Date;
                DateTime toDate = DateTime.UtcNow;

                int itemLifeMinutes = Convert.ToInt32(fromDate.Subtract(toDate).TotalMinutes);
                return itemLifeMinutes;
            }
        }

        public MemoryCache()
        {
            cache = System.Runtime.Caching.MemoryCache.Default;
        }

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public void Add(string key, object value)
        {
            CacheItemPolicy newCacheItemPolicy = new CacheItemPolicy();
            newCacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(ItemLifeMinutes);
            cache.Add(new CacheItem(key, value), newCacheItemPolicy);
        }
        public void Remove(string key)
        {
            cache.Remove(key);
        }
        public T Get<T>(string key)
        {
            return (T)cache.Get(key);
        }

    }
}
