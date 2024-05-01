using DDDDemo.Event;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(ILogger<TestController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly IMediator mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        await mediator.Publish(new TestEvent(req.UserName));
        return Ok("ok");
    }
}
public record LoginRequest(string UserName, string Password);