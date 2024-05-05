namespace Users.Domain.Entities;

public record UserAccessFail
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }

    private bool lockOut;   // 是否锁定
    public DateTime? LockOutEnd { get; private set; }   // 锁定结束期
    public int AccessFailedCount { get; private set; }   // 登录失败次数

    public UserAccessFail(User user)
    {
        Id = Guid.NewGuid();
        this.User = user;
    }
    /// <summary>
    /// 重置登录失败信息
    /// </summary>
    public void Reset()
    {
        lockOut = false;
        LockOutEnd = null;
        AccessFailedCount = 0;
    }


    public bool IsLockedOut()
    {
        if (lockOut)
        {
            if (LockOutEnd >= DateTime.Now)
            {
                return true;
            }
            // 锁定到期

            AccessFailedCount = 0;
            LockOutEnd = null;
            return false;
        }
        return false;
    }

    public void Fail()
    {
        AccessFailedCount++;
        if (AccessFailedCount>=3)
        {
            lockOut = true;
            LockOutEnd = DateTime.Now.AddMinutes(5);

        }
    }
}
