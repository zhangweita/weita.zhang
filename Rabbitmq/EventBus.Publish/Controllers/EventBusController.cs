using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zack.EventBus;

namespace EventBus.Publish.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventBusController(IEventBus eventBus) : ControllerBase
{
    private readonly IEventBus eventBus = eventBus;

    [HttpPost]
    public Task Publish(RegisterUserRequest req)
    {
        eventBus.Publish("UserAdded", req);
        return Task.CompletedTask;
    }
}

public record RegisterUserRequest(string UserName, int Age);


