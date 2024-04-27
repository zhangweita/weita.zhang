using ApiDemo.Common;
using ApiDemo.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDemo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class Test2Controller(ILogger<Test2Controller> logger, UserManager<User> userManager, IOptionsSnapshot<JWTOptions> jwtOptions) : ControllerBase
{
    private readonly ILogger<Test2Controller> logger = logger;
    private readonly UserManager<User> userManager = userManager;
    private readonly IOptionsSnapshot<JWTOptions> jwtOptions = jwtOptions;

    [HttpGet]
    public IActionResult Hello()
    {
        string id = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        string userName = this.User.FindFirst(ClaimTypes.Name)!.Value;
        IEnumerable<Claim> roleClaims = this.User.FindAll(ClaimTypes.Role);

        string roleNames = string.Join(',', roleClaims.Select(c => c.Value));

        return Ok($"id={id},userName={userName},roleNames={roleNames}");
    }
}

