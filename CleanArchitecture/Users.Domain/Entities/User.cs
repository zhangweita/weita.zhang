namespace Users.Domain.Entities;

public record User : IAggregateRoot
{
    public Guid Id { get; init; }
    public PhoneNumber PhoneNumber { get; private set; }    // 手机号
    private string? passwordHash;
    public UserAccessFail AccessFail { get; private set; }
    public User(PhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        PhoneNumber = phoneNumber;
        this.AccessFail = new UserAccessFail(this);
    }
    /// <summary>
    /// 是否设置密码
    /// </summary>
    /// <returns></returns>
    public bool HasPassword() => !string.IsNullOrEmpty(passwordHash);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException"></exception>
    public void ChangePassword(string value)
    {
        if (value.Length <= 3) throw new ArgumentException("密码长度不能小于3");

        passwordHash = HashHelper.ComputeMd5Hash(value);
    }

    /// <summary>
    /// 检查密码是否正确
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool CheckPassword(string password) => passwordHash == HashHelper.ComputeMd5Hash(password);
    /// <summary>
    /// 修改手机号
    /// </summary>
    /// <param name="phoneNumber"></param>
    public void ChangePhoneNumber(PhoneNumber phoneNumber) => PhoneNumber = phoneNumber;
}
