

namespace Services.Interfaces;

public interface ICacheService
{
    /// <summary>
    /// Gets an item from the cache. If it doesn't exist, executes the factory method to get it, caches it, and returns it.
    /// </summary>
    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);

    /// <summary>
    /// Removes an item from the cache by its key.
    /// </summary>
    void Remove(string key);
}
