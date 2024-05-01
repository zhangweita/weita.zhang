using MediatR;

namespace DDDDemo.Event;

public record TestEvent(string UserName) : INotification;

public class TestEventHandler1 : INotificationHandler<TestEvent>
{
    public Task Handle(TestEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"我收到了{notification.UserName}");
        return Task.CompletedTask;
    }
}

public class TestEventHandler2 : INotificationHandler<TestEvent>
{
    public async Task Handle(TestEvent notification, CancellationToken cancellationToken)
    {
        await File.WriteAllTextAsync($"d:/1.txt", $"{notification.UserName}来了");
    }
}