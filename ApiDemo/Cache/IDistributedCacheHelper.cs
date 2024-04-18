using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace ApiDemo.Cache;

public interface IDistributedCacheHelper
{
    T? GetOrCreate<T>(string key, Func<DistributedCacheEntryOptions, T?> valueFactory, int expireSeconds = 60);
    Task<T?> GetOrCreateAsync<T>(string key, Func<DistributedCacheEntryOptions, Task<T?>> valueFactory, int expireSeconds = 60);
    void Remove(string cacheKey);
    Task RemoveAsync(string cacheKey);
}
