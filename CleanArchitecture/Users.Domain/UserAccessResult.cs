namespace Users.Domain;


public enum UserAccessResult
{
    OK, //成功
    PhoneNumberNotFound, //手机号不存在
    Lockout, //用户被锁定
    NoPassword,//账户未设置密码
    PasswordError//密码错误
}