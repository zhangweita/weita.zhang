using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ApiDemo.Filters;

/// <summary>
/// 限流拦截器
/// </summary>
/// <param name="memoryCache"></param>
public class RateLimitFilter(IMemoryCache memoryCache) : IAsyncActionFilter
{
    private readonly IMemoryCache memoryCache = memoryCache;

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string remoteIp = context.HttpContext.Connection.RemoteIpAddress!.ToString();
        string cacheKey = $"LastVisitTick_{remoteIp}";
        long? lastTick = memoryCache.Get<long?>(cacheKey);
        if (lastTick == null || Environment.TickCount64 - lastTick > 1000)
        {
            memoryCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));
            return next();
        }
        else
        {
            context.Result = new ContentResult { StatusCode = 429 };
            return Task.CompletedTask;
        }
    }
}
