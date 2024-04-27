using ApiDemo.Cache;
using ApiDemo.Common;
using ApiDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDemo.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController(ILogger<TestController> logger, RoleManager<Role> roleManager, UserManager<User> userManager,
IOptionsSnapshot<JWTOptions> jwtOptions) : ControllerBase
{
    private readonly ILogger<TestController> logger = logger;
    private readonly RoleManager<Role> roleManager = roleManager;
    private readonly UserManager<User> userManager = userManager;
    private readonly IOptionsSnapshot<JWTOptions> jwtOptions = jwtOptions;

    [HttpPost]
    public async Task<IActionResult> Login2(LoginRequest req)
    {
        string userName = req.UserName;
        string password = req.Password;
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return NotFound($"用户名{userName}不存在");
        }
        if (await userManager.IsLockedOutAsync(user))
        {
            return BadRequest("Locked Out");
        }

        var success = await userManager.CheckPasswordAsync(user, password);
        if (!success)
        {
            await userManager.AccessFailedAsync(user);
            return Unauthorized("Login Failed");
        }

        user.JWTVersion++;
        await userManager.UpdateAsync(user);
        List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Version, user.JWTVersion.ToString()),
            ];
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        return Ok(BuildToken(claims, jwtOptions.Value));
    }

    private static string BuildToken(List<Claim> claims, JWTOptions jwtOptions)
    {
        DateTime expires = DateTime.Now.AddSeconds(jwtOptions.ExpireSeconds);

        byte[] secBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey!);
        SymmetricSecurityKey securityKey = new(secBytes);
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new(claims: claims, expires: expires, signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserRole(string userName, string password, string roleName)
    {
        bool roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            Role role = new() { Name = roleName };
            var r = await roleManager.CreateAsync(role);
            if (!r.Succeeded)
            {
                return BadRequest(r.Errors);
            }
        }

        User? user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new() { UserName = userName, Email = $"{userName}@foxmail.com", EmailConfirmed = true };
            var r = await userManager.CreateAsync(user, password ?? userName);
            if (!r.Succeeded)
            {
                return BadRequest(r.Errors);
            }
            r = await userManager.AddToRoleAsync(user, roleName);
            if (!r.Succeeded)
            {
                return BadRequest(r.Errors);
            }
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SendResetPasswordToken(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        string token = await userManager.GeneratePasswordResetTokenAsync(user!);

        logger.LogInformation($"向邮箱{email}发送Token={token}");

        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> VerifyResetPasswordToken(ResetPasswordRequest req)
    {
        string email = req.Email;
        var user = await userManager.FindByEmailAsync(email);
        string token = req.Token;
        string password = req.NewPassword;
        var r = await userManager.ResetPasswordAsync(user!, token, password);
        if (!r.Succeeded)
        {
            return BadRequest(r.Errors);
        }

        return Ok();
    }

    const string key = "[eQf4jA&}syCoMlI-fF0]{!fA@!d!l";

    //[HttpPost]
    //public string CreateJwtToken(UserInfo userInfo)
    //{
    //    List<Claim> claims =
    //    [
    //        new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
    //        new(ClaimTypes.Name, userInfo.Name),
    //        new("Passport",userInfo.Passport),
    //    ];

    //    userInfo.Roles.ToList().ForEach(x => claims.Add(new(ClaimTypes.Role, x)));

    //    DateTime expires = DateTime.Now.AddDays(1);

    //    byte[] secBytes = Encoding.UTF8.GetBytes(key);
    //    SymmetricSecurityKey securityKey = new(secBytes);
    //    SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

    //    JwtSecurityToken token = new(claims: claims, expires: expires, signingCredentials: credentials);

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    //[HttpPost]
    //public string ValidateJwtToken(string jwt)
    //{
    //    JwtSecurityTokenHandler tokenHandler = new();
    //    TokenValidationParameters validationParameters = new()
    //    {
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
    //        ValidateIssuer = false,
    //        ValidateAudience = false
    //    };


    //    ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken token);

    //    return string.Join(" ", claimsPrincipal.Claims.Select(x => $"{x.Type}={x.Value}"));
    //}


    //[HttpPost]
    //public string GetDecodeJwtString(string jwt)
    //{
    //    string[] segments = jwt.Split('.');

    //    StringBuilder sb = new();
    //    sb.AppendLine("Jwt Header:");
    //    sb.AppendLine(JwtDecode(segments[0]));
    //    sb.AppendLine("Jwt Payload:");
    //    sb.AppendLine(JwtDecode(segments[1]));
    //    sb.AppendLine("Jwt Signatue:");
    //    sb.AppendLine(JwtDecode(segments[2]));

    //    return sb.ToString();
    //}

    //private string JwtDecode(string s)
    //{
    //    s = s.Replace('-', '+').Replace('_', '/');
    //    s += (s.Length % 4) switch
    //    {
    //        2 => "==",
    //        3 => "=",
    //        _ => default
    //    };

    //    var bytes = Convert.FromBase64String(s);
    //    return Encoding.UTF8.GetString(bytes);
    //}
}
public record UserInfo(string Id, string Name, string[] Roles, string Passport);

public record Person(int Id, string Name, int Age);
public record Student(int Id, string Name, int Age, string schoolName);
public record ErrorInfo(int Code, string? Message);
public record LoginResult(bool IsOK, ProcessInfo[]? Processes);
public record LoginRequest(string UserName, string Password);
public record ResetPasswordRequest(string Email, string Token, string NewPassword);
public record ProcessInfo(int Id, string ProcessName, long WorkingSet6);