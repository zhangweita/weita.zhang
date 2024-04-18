using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ApiDemo.Cache
{
    public class DistributedCacheHelper(IDistributedCache distributedCache) : IDistributedCacheHelper
    {
        private readonly IDistributedCache distributedCache = distributedCache;
        public T? GetOrCreate<T>(string key, Func<DistributedCacheEntryOptions, T?> valueFactory, int expireSeconds = 60)
        {
            string? jsonStr = distributedCache.GetString(key);
            if (string.IsNullOrEmpty(jsonStr))
            {
                var options = CreateOptions(expireSeconds);
                T? result = valueFactory(options);

                string jsonOfResult = JsonConvert.SerializeObject(result);

                distributedCache.SetString(key, jsonOfResult, options);
            }
            else
            {
                distributedCache.Refresh(key);
            }
            return JsonConvert.DeserializeObject<T>(jsonStr!);
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<DistributedCacheEntryOptions, Task<T?>> valueFactory, int expireSeconds = 60)
        {
            string? jsonStr = await distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(jsonStr))
            {
                var options = CreateOptions(expireSeconds);
                T? result = await valueFactory(options);

                string jsonOfResult = JsonConvert.SerializeObject(result);

                await distributedCache.SetStringAsync(key, jsonOfResult, options);
            }
            else
            {
                await distributedCache.RefreshAsync(key);
            }
            return JsonConvert.DeserializeObject<T>(jsonStr!);
        }

        public void Remove(string cacheKey) => distributedCache.Remove(cacheKey);

        public async Task RemoveAsync(string cacheKey) => await distributedCache.RemoveAsync(cacheKey);

        private DistributedCacheEntryOptions CreateOptions(int baseExpireSeconds)
        {
            double expire = Random.Shared.Next(baseExpireSeconds, baseExpireSeconds * 2);
            TimeSpan expiration = TimeSpan.FromSeconds(expire);
            DistributedCacheEntryOptions options = new() { AbsoluteExpirationRelativeToNow = expiration };

            return options;
        }
    }
}
