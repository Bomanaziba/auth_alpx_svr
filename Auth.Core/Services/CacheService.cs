

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Primitives;
using System.Runtime.Caching;

namespace Auth.Core.Services
{

    public class CacheService
    {
        private static MemoryCache _memoryCache => MemoryCache.Default;

        protected static string BuildCacheKey<T>()
        {
            return $"AX::Caching::{typeof(T).Name}";
        }

        public static T Get<T>(string key) where T : class
        {
            return _memoryCache.Get(key) as T;
        }

        public static void Add<T>(string key, T data, double? expiration = null) where T : class
        {
            var policy = new CacheItemPolicy();

            policy.AbsoluteExpiration =  DateTimeOffset.Now.AddDays(60.0);

            _memoryCache.Set(key, data, policy);
        }

        public static void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

    }
}
