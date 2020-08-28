using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecsys.Retail.Caching;
using System.Runtime.Caching;
using System.Configuration;

namespace Tecsys.Retail.Caching
{
    public class TsCache:ITsCache
    {
        object _cacheLock = new object();
        //Cache regions


        /// <summary>
        /// adds or updates a cache item
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        /// <param name="cacheRegion"></param>
        public void AddOrUpdate(string cacheKey, dynamic obj)
        {
            try
            {
                ObjectCache cache = MemoryCache.Default;
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();

                AppSettingsReader appSettings = new AppSettingsReader();
                int cacheExpirationMinutes = (int)appSettings.GetValue("CacheExpirationMinutes", typeof(int));

                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(cacheExpirationMinutes);

                CacheItem cacheItem = new CacheItem(cacheKey, obj);

                lock (_cacheLock)
                {
                    cache.Set(cacheItem, cacheItemPolicy);
                }
            }
            catch
            {
                //log
                throw;
            }
        }

        public dynamic Get(string cacheKey)
        {
            ObjectCache cache = MemoryCache.Default;
            try
            {
                lock (_cacheLock)
                {
                    if (cache.Contains(cacheKey))
                        return cache.Get(cacheKey);
                }
            }
            catch
            {
                //log
                throw;
            }

            return null;
        }
    }
}
