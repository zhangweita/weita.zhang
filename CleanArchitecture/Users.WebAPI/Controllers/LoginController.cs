using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Users.Domain;
using Users.Domain.Entities;
using Users.Infrastructure;

namespace Users.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[UnitOfWork(typeof(UserDbContext))]
public class LoginController(UserDomainService userDomainService, IUserDomainRepository repository,
                                UserDbContext userDbContext) : ControllerBase
{
    private readonly UserDomainService userDomainService = userDomainService;
    private readonly IUserDomainRepository repository = repository;
    private readonly UserDbContext userDbContext = userDbContext;

    public async Task<IActionResult> LoginByPhoneAndPwd(LoginByPhoneAndPwdRequest req)
    {
        if (req.Password.Length < 3) return BadRequest("密码的长度不能小于3");

        var phoneNumber = req.PhoneNumber;
        var result = await userDomainService.CheckLoginAsync(phoneNumber, req.Password);
        return result switch
        {
            UserAccessResult.OK => Ok("登录成功"),
            UserAccessResult.Lockout => BadRequest("用户被锁定，请稍后再试"),
            UserAccessResult.PhoneNumberNotFound or UserAccessResult.NoPassword or UserAccessResult.PasswordError => BadRequest("手机号或者密码错误"),
            _ => throw new NotImplementedException(),
        };
    }

    public async Task<IActionResult> AddNew(PhoneNumber phoneNumber)
    {
        if (null != await repository.FindOneAsync(phoneNumber))
        {
            return BadRequest("手机号已经存在");
        }

        User user = new(phoneNumber);
        userDbContext.Users.Add(user);
        return Ok("成功");
    }
}

public record LoginByPhoneAndPwdRequest(PhoneNumber PhoneNumber, string Password);