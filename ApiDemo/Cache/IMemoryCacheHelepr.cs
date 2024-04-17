using Microsoft.Extensions.Caching.Memory;

namespace ApiDemo.Cache;

public interface IMemoryCacheHelepr
{
    T? GetOrCreate<T>(string key, Func<ICacheEntry, T?> valueFactory, int expireSeconds = 60);
    Task<T?> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T?>> valueFactory, int expireSeconds = 60);
    void Remove(string cacheKey);
}
