using MediatR;
using Users.Domain;
using Users.Domain.Events;

namespace Users.WebAPI.Events;

public class UserAccessResultEventHandler(IUserDomainRepository repository) : INotificationHandler<UserAccessResultEvent>
{
    private readonly IUserDomainRepository repository = repository;

    public Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
    {
        var result = notification.Result;

        var phoneNumber = notification.PhoneNumber;
        string msg = result switch
        {
            UserAccessResult.OK => $"{phoneNumber}登录成功",
            UserAccessResult.PhoneNumberNotFound => $"{phoneNumber}登录失败，用户不存在",
            UserAccessResult.PasswordError => $"{phoneNumber}登录失败，密码错误",
            UserAccessResult.NoPassword => $"{phoneNumber}登录失败，未设置密码",
            UserAccessResult.Lockout => $"{phoneNumber}登录失败，用户被锁定",
            _ => throw new NotImplementedException()
        };

        return repository.AddNewLoginHistoryAsync(phoneNumber, msg);
    }
}
