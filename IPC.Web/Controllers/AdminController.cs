using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPC.Presentation.Web.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> _logger)
    {
        this._logger = _logger;
    }

    [Authorize]
    public IActionResult Index()
    {
        _logger.LogError("这是个大大滴异常信息");
        return View();
    }
}
