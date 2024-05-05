namespace Users.Domain.Entities;

public record UserLoginHistory : IAggregateRoot
{
    public long Id { get; init; }

    public Guid? UserId { get; init; }
    public PhoneNumber PhoneNumber { get; init; }
    public DateTime CreateDateTime { get; init; }
    public string Message { get; init; }
    private UserLoginHistory() { }

    public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        CreateDateTime = DateTime.Now;
        Message = message;
    }
}
