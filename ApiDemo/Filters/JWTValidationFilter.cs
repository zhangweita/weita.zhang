using ApiDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Security.Claims;

namespace ApiDemo.Filters;

public class JWTValidationFilter(IMemoryCache memoryCache, UserManager<User> userManager) : IAsyncActionFilter
{
    private readonly IMemoryCache memoryCache = memoryCache;
    private readonly UserManager<User> userManager = userManager;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimUserId == null)
        {
            await next();
            return;
        }

        long userId = long.Parse(claimUserId.Value);
        string cacheKey = $"JWTValidationFilter.UserInfo.{userId}";

        User? user = await memoryCache.GetOrCreateAsync(cacheKey, async e =>
        {
            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
            return await userManager.FindByIdAsync(userId.ToString());
        });

        if (user == null)
        {
            ObjectResult result = new($"UserId({userId}) not found");
            result.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = result;
            return;
        }

        var claimVersion = context.HttpContext.User.FindFirst(ClaimTypes.Version);

        long jwtVerOfReq = long.Parse(claimVersion!.Value);

        if (jwtVerOfReq >= user.JWTVersion)
        {
            await next();
        }
        else
        {
            ObjectResult result = new($"JWTVersion mismatch");
            result.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = result;
            return;
        }

    }
}
