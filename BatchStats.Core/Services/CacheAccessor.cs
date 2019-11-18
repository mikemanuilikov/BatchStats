using BatchStats.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BatchStats.Core.Services
{
    public class CacheAccessor : ICacheAccessor
    {
        private readonly IMemoryCache cache;

        public CacheAccessor(IMemoryCache memoryCache)
        {
            cache = memoryCache;
        }

        public void Store<TValue>(string key, TValue value, TimeSpan expiration)
        {
            cache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });
        }

        public bool TryGet<TValue>(string key, out TValue value)
        {
            return cache.TryGetValue(key, out value);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}