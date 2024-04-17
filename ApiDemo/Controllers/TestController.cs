using BooksEFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ApiDemo.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController(IMemoryCache memoryCache, MyDbContext dbContext, ILogger<TestController> logger) : ControllerBase
{
    private readonly IMemoryCache memoryCache = memoryCache;
    private readonly ILogger<TestController> logger = logger;
    private readonly MyDbContext dbContext = dbContext;

    [HttpGet]
    public async Task<Book[]> GetBooks()
    {
        logger.LogInformation("开始执行GetBooks");
        var items = await memoryCache.GetOrCreateAsync("AllBooks", async e =>
          {
              e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
              logger.LogInformation("从数据库中读取数据");
              return await dbContext.Books.ToArrayAsync();
          });

        logger.LogInformation("把数据返回给调用者");
        return items!;
    }


    [HttpGet]
    public ActionResult<Person> GetPerson(int id) => id switch
    {
        <= 0 => BadRequest("id必须是正数"),
        1 => new Person(1, "洪洪洪", 18),
        2 => new Person(2, "刁刁刁", 18),
        3 => new Person(2, "旺旺旺", 18),
        _ => NotFound(new ErrorInfo(2, "人员不存在"))
    };

    [HttpGet("/school/{schoolName}/class/{classNo}")]
    public ActionResult<Student[]> GetAll(string schoolName, [FromRoute(Name = "classNo")] int classNum)
    {
        return NotFound();
    }

    [HttpPost]
    public ActionResult<LoginResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest.UserName == "admin" && loginRequest.Password == "123456")
        {
            var processes = Process.GetProcesses().Select(p => new ProcessInfo(
                                                            p.Id, p.ProcessName, p.WorkingSet64)).ToArray();
            return new LoginResult(true, processes);
        }
        else
        {
            return new LoginResult(false, null);
        }
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public DateTime Now() => DateTime.Now;
}

public record Person(int Id, string Name, int Age);
public record Student(int Id, string Name, int Age, string schoolName);
public record ErrorInfo(int Code, string? Message);
public record LoginResult(bool IsOK, ProcessInfo[]? Processes);
public record LoginRequest(string UserName, string Password);
public record ProcessInfo(int Id, string ProcessName, long WorkingSet6);