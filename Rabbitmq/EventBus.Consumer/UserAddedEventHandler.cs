using Zack.EventBus;

namespace EventBus.Consumer;

[EventName("UserAdded")]
public class UserAddedEventHandler1(ILogger<UserAddedEventHandler1> logger) : IIntegrationEventHandler
{
    private readonly ILogger<UserAddedEventHandler1> logger = logger;

    public Task Handle(string eventName, string eventData)
    {
        logger.LogInformation($"新建了用户：{eventData}");
        return Task.CompletedTask;
    }
}



[EventName("UserAdded")]
public class UserAddedEventHandler2(ILogger<UserAddedEventHandler2> logger) : DynamicIntegrationEventHandler
{
    private readonly ILogger<UserAddedEventHandler2> logger = logger;

    public override Task HandleDynamic(string eventName, dynamic eventData)
    {
        logger.LogInformation($"新建了用户：{eventData}");
        return Task.CompletedTask;
    }
}


public record RegisterUserRequest(string UserName, int Age);

[EventName("UserAdded")]
public class UserAddedEventHandler3(ILogger<UserAddedEventHandler3> logger) : JsonIntegrationEventHandler<RegisterUserRequest>
{
    private readonly ILogger<UserAddedEventHandler3> logger = logger;

    public override Task HandleJson(string eventName, RegisterUserRequest? eventData)
    {
        logger.LogInformation($"新建了用户：{eventData}");
        return Task.CompletedTask;
    }
}