using Users.Domain.Entities;

namespace Users.Domain.Events;

public class UserAccessResultEvent(PhoneNumber phoneNum, UserAccessResult result)
{
    private PhoneNumber phoneNum = phoneNum;
    private UserAccessResult result = result;
}
