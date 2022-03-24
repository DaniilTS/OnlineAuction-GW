using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace DBAL
{
    public class Cache<T>
    {
        private readonly MemoryCache _cache = new(new MemoryCacheOptions());

        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks = new();

        private readonly Func<Task<T>> _createItem;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        public Cache(Func<Task<T>> createItem, TimeSpan timeSpan)
        {
            _createItem = createItem;
            _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(timeSpan);
        }

        public async Task<T> this[object key] => GetOrCreate(key);

        public async Task<T> GetOrCreate(object key)
        {
            if (_cache.TryGetValue(key, out T cacheEntry))
                return cacheEntry;

            var locker = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

            await locker.WaitAsync();
            try
            {
                if (!_cache.TryGetValue(key, out cacheEntry))
                {
                    cacheEntry = await _createItem();

                    _cache.Set(key, cacheEntry, _cacheEntryOptions);
                }
            }
            finally
            {
                locker.Release();
            }

            return cacheEntry;
        }
    }
}
