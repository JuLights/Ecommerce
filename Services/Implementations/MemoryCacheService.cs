

using Microsoft.Extensions.Caching.Memory;
using Services.Interfaces;

namespace Services.Implementations;

public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        // Check if the item is already in the cache
        if (memoryCache.TryGetValue(key, out T? cachedValue))
        {
            return cachedValue;
        }

        // If not, execute the database call (factory)
        var value = await factory();

        if (value != null)
        {
            // Set cache options (default to 1 hour if not specified)
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(1)
            };

            // Save data in cache
            memoryCache.Set(key, value, cacheEntryOptions);
        }

        return value;
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }
}
