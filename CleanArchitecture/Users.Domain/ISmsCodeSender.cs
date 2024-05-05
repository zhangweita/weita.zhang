using Users.Domain.Entities;

namespace Users.Domain;

public interface ISmsCodeSender
{
    /// <summary>
    /// 发送短信验证码
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task SendCodeAsync(PhoneNumber phoneNumber, string code);
}
