using DDDDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DDDDemo.Controllers;

public class UsersController(ILogger<UsersController> logger, UserDbContext context) : ControllerBase
{
    private readonly ILogger<UsersController> logger = logger;
    private readonly UserDbContext context = context;

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest req)
    {
        User? user = await context.Users.FindAsync(id);
        user!.ChangeAge(req.Age);
        user.ChangeEmail(req.Email);
        user.ChangeNickName(req.NickName);
        await context.SaveChangesAsync();
        return Ok();
    }
}

public record UpdateUserRequest(int Age, string Email, string NickName);