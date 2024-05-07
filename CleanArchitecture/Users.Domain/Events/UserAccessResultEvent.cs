using MediatR;
using Users.Domain.Entities;

namespace Users.Domain.Events;

public class UserAccessResultEvent(PhoneNumber phoneNum, UserAccessResult result) : INotification
{
    public PhoneNumber PhoneNumber { get; init; } = phoneNum;
    public UserAccessResult Result { get; init; } = result;
}
