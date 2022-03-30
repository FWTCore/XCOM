using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Cache
{
    public static class XMMemoryCache
    {
        public readonly static IMemoryCache _memoryCache;

        static XMMemoryCache()
        {
            _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        }

        #region 常规缓存
        /// <summary>
        /// 获取缓存的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Object GetCache(string key)
        {
            return _memoryCache.Get(key);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static T SetCache<T>(string key, T value)
        {
            return _memoryCache.GetOrCreate(key, entry =>
            {
                return value;
            });
        }
        /// <summary>
        /// 设置缓存(固定时间过期)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        public static T SetCacheAbsolute<T>(string key, T value, int absoluteExpiration)
        {
            return _memoryCache.GetOrCreate(key, entry =>
             {
                 entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpiration));
                 return value;
             });
        }
        /// <summary>
        /// 设置缓存(滚动时间过期)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        public static T SetCacheSliding<T>(string key, T value, int slidingExpiration)
        {
            return _memoryCache.GetOrCreate(key, entry =>
             {
                 entry.SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpiration));
                 return value;
             });
        }

        #endregion


    }
}
