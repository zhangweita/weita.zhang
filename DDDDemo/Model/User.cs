
using MediatR;

namespace DDDDemo.Model;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public string UserName { get; init; }
    public string Email { get; private set; }
    public string? NickName { get; private set; }
    public int Age { get; private set; }
    public bool IsDeleted { get; private set; }
    private User() { }

    public User(string userName, string email)
    {
        this.Id = Guid.NewGuid();
        this.UserName = userName;
        this.Email = email;
        this.IsDeleted = false;

        AddDomainEvent(new UserAddedEvent(this));
    }

    public void ChangeNickName(string? value)
    {
        this.NickName = value;
        AddDomainEventIfAbsent(new UserUpdatedEvent(Id));
    }
    public void ChangeEmail(string value)
    {
        this.Email = value;
        AddDomainEventIfAbsent(new UserUpdatedEvent(Id));
    }
    public void ChangeAge(int value)
    {
        this.Age = value;
        AddDomainEventIfAbsent(new UserUpdatedEvent(Id));
    }

    public void SoftDelete()
    {
        this.IsDeleted = true;
        AddDomainEvent(new UserSoftDeletedEvent(Id));
    }

}

public record UserAddedEvent(User User) : INotification;
public class NewUserSendEmailHandler(ILogger<NewUserSendEmailHandler> logger) : INotificationHandler<UserAddedEvent>
{
    private readonly ILogger<NewUserSendEmailHandler> logger = logger;

    public Task Handle(UserAddedEvent notification, CancellationToken cancellationToken)
    {
        User user = notification.User;
        logger.LogInformation($"向{user}发送欢迎邮件");
        return Task.CompletedTask;
    }
}

public record UserUpdatedEvent(Guid Id) : INotification;
public class ModifyUserLogHandler(ILogger<ModifyUserLogHandler> logger, UserDbContext context) : INotificationHandler<UserUpdatedEvent>
{
    private readonly ILogger<ModifyUserLogHandler> logger = logger;
    private readonly UserDbContext context = context;

    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        User? user = await context.Users.FindAsync(notification.Id);
        logger.LogInformation($"通知用户{user.Email}的信息被修改了");
    }
}


public record UserSoftDeletedEvent(Guid Id) : INotification;