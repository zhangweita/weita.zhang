using Users.Domain.Entities;
using Users.Domain.Events;

namespace Users.Domain;

[UnitOfWork]
public class UserDomainService(IUserDomainRepository repository, ISmsCodeSender smsSender)
{
    private readonly IUserDomainRepository repository = repository;
    private readonly ISmsCodeSender smsSender = smsSender;

    public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNum, string password)
    {
        User? user = await repository.FindOneAsync(phoneNum);
        UserAccessResult result;
        if (user == null)
            result = UserAccessResult.PhoneNumberNotFound;
        else if (IsLockOut(user))
            result = UserAccessResult.Lockout;
        else if (user.HasPassword() == false)
            result = UserAccessResult.NoPassword;
        else if (user.CheckPassword(password))
            result = UserAccessResult.OK;
        else
            result = UserAccessResult.PasswordError;

        if (user != null)
        {
            if (result == UserAccessResult.OK)
                this.ResetAccessFail(user); //重置
            else
                this.AccessFail(user); //处理登录失败
        }

        // 登录失败日志交由领域事件发布
        await repository.PublishEventAsync(new UserAccessResultEvent(phoneNum, result));
        return result;
    }

    public async Task<UserAccessResult> SendCodeAsync(PhoneNumber phoneNumber)
    {
        var user = await repository.FindOneAsync(phoneNumber);
        if (user == null) return UserAccessResult.PhoneNumberNotFound;
        if (IsLockOut(user)) return UserAccessResult.Lockout;

        string code = Random.Shared.Next(1000, 9999).ToString();

        await repository.SavePhoneCodeAsync(phoneNumber, code);
        await smsSender.SendCodeAsync(phoneNumber, code);

        return UserAccessResult.OK;
    }

    public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phoneNumber, string code)
    {
        var user = await repository.FindOneAsync(phoneNumber);
        if (user == null) return CheckCodeResult.PhoneNumberNotFound;
        if (IsLockOut(user)) return CheckCodeResult.Lockout;

        string? codeInServer = await repository.RetrivePhoneCodeAsync(phoneNumber);

        if (string.IsNullOrEmpty(codeInServer)) return CheckCodeResult.CodeError;

        if (code == codeInServer) return CheckCodeResult.OK;
        else
        {
            AccessFail(user);
            return CheckCodeResult.CodeError;
        }
    }


    private void AccessFail(User user)
    {
        user.AccessFail.Fail();
    }

    private void ResetAccessFail(User user)
    {
        user.AccessFail.Reset();
    }

    private bool IsLockOut(User user) => user.AccessFail.IsLockedOut();
}

