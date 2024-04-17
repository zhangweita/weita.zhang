using Microsoft.Extensions.Caching.Memory;
using System.Collections;

namespace ApiDemo.Cache;

public class MemoryCacheHelepr(IMemoryCache memoryCache) : IMemoryCacheHelepr
{
    private readonly IMemoryCache memoryCache = memoryCache;

    /// <summary>
    /// 获取指定key缓存值，若不存在则根据给定规则添加缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <param name="expireSeconds"></param>
    /// <returns></returns>
    public T? GetOrCreate<T>(string key, Func<ICacheEntry, T?> valueFactory, int expireSeconds = 60)
    {
        ValidateType<T>();
        if (!memoryCache.TryGetValue(key, out T? value))
        {
            using ICacheEntry entry = memoryCache.CreateEntry(key);
            InitCacheKey(entry, expireSeconds);
            value = valueFactory(entry);
            entry.Value = value;
        }
        return value;
    }
    /// <summary>
    /// 获取指定key缓存值，若不存在则根据给定规则添加缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <param name="expireSeconds"></param>
    /// <returns></returns>
    public async Task<T?> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T?>> valueFactory, int expireSeconds = 60)
    {
        ValidateType<T>();
        if (!memoryCache.TryGetValue(key, out T? value))
        {
            using ICacheEntry entry = memoryCache.CreateEntry(key);
            InitCacheKey(entry, expireSeconds);
            value = await valueFactory(entry);
            entry.Value = value;
        }
        return value;
    }
    /// <summary>
    /// 删除缓存
    /// </summary>
    /// <param name="cacheKey"></param>
    public void Remove(string cacheKey) => memoryCache.Remove(cacheKey);

    /// <summary>
    /// 验证缓存值是否为懒加载类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="InvalidOperationException"></exception>
    private static void ValidateType<T>()
    {
        Type type = typeof(T);
        if (type.IsGenericType)
        {
            type = type.GetGenericTypeDefinition();
        }
        Type[] lazyLoadType = [typeof(IEnumerable<>), typeof(IEnumerable),
                                typeof(IAsyncEnumerable<T>), typeof(IQueryable<T>), typeof(IQueryable)];
        if (lazyLoadType.Contains(type))
        {
            string typeName = typeof(T).Name;
            throw new InvalidOperationException($"Please use List<{typeName}> or {typeName}[] instead.");
        }
    }
    /// <summary>
    /// 设置缓存过期时间
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="baseExpireSeconds"></param>
    private static void InitCacheKey(ICacheEntry entry, int baseExpireSeconds)
    {
        double expire = Random.Shared.Next(baseExpireSeconds, baseExpireSeconds * 2);
        TimeSpan expiration = TimeSpan.FromSeconds(expire);
        entry.AbsoluteExpirationRelativeToNow = expiration;
    }
}
