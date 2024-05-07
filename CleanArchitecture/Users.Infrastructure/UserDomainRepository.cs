using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Users.Domain;
using Users.Domain.Entities;
using Users.Domain.Events;

namespace Users.Infrastructure;

public class UserDomainRepository(UserDbContext dbContext, IDistributedCache distributedCache, IMediator mediator) : IUserDomainRepository
{
    private readonly UserDbContext dbContext = dbContext;
    private readonly IDistributedCache distributedCache = distributedCache;
    private readonly IMediator mediator = mediator;

    public Task<User?> FindOneAsync(PhoneNumber phoneNumber)
    {
        return dbContext.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public Task<User?> FindOneAsync(Guid userId)
    {
        return dbContext.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string msg)
    {
        var user = await FindOneAsync(phoneNumber);
        UserLoginHistory history = new(user?.Id, phoneNumber, msg);
        dbContext.LoginHistories.Add(history);
    }

    public Task PublishEventAsync(UserAccessResultEvent eventData)
    {
        return mediator.Publish(eventData);
    }

    public Task<string?> RetrivePhoneCodeAsync(PhoneNumber phoneNumber)
    {
        string fullNumber = phoneNumber.RegionCode + phoneNumber.Number;
        string cacheKey = $"LoginByPhoneAndCode_Code_{fullNumber}";
        string? code = distributedCache.GetString(cacheKey);
        distributedCache.Remove(cacheKey);
        return Task.FromResult(code);
    }

    public Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code)
    {
        string fullNumber = phoneNumber.RegionCode + phoneNumber.Number;
        DistributedCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
        };
        distributedCache.SetString($"LoginByPhoneAndCode_Code_{fullNumber}", code, options);
        return Task.CompletedTask;
    }
}
