using Microsoft.Extensions.Logging;
using Users.Domain;
using Users.Domain.Entities;

namespace Users.Infrastructure;

public class MockSmsCodeSender(ILogger<MockSmsCodeSender> logger) : ISmsCodeSender
{
    private readonly ILogger<MockSmsCodeSender> logger = logger;

    public Task SendCodeAsync(PhoneNumber phoneNumber, string code)
    {
        logger.LogInformation($"发送验证码：{code} 至手机号码：{phoneNumber}");
        return Task.CompletedTask;
    }
}
