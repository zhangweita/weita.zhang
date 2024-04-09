using IPC.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPC.Web.Controllers;

public class ApiLogController : Controller
{
    private readonly ILogger<ApiLogController> _logger;
    private readonly ApiLogService _apiLogService;
    public ApiLogController(ILogger<ApiLogController> _logger, ApiLogService _apiLogService)
    {
        this._logger = _logger;
        this._apiLogService = _apiLogService;
    }

    public async Task<IActionResult> Index(int pageNum = 1)
    {
        int pageSize = 50;
        var logList = await _apiLogService.GetLogListAsync(pageNum, ViewBag.PageSize);

        ViewBag.PageIndex = pageNum;
        ViewBag.PageSize = pageSize;
        ViewBag.RecordCount = logList.Count;
        ViewBag.PageCount = Math.Ceiling(logList.Count() * 1.0 / pageSize);

        return View(logList);
    }

    //public async Task<IActionResult> GetLogListAsync(int pageNum)
    //{
    //    ViewBag.PageSize = ViewBag.PageSize ?? 50;
    //    var logList = await _apiLogService.GetLogListAsync(pageNum, ViewBag.PageSize);
    //    return View(logList);
    //}
    //public async Task<IActionResult> GetPreviousAsync() => await GetLogListAsync(ViewBag.PageNum - 1);
    //public async Task<IActionResult> GetNextAsync() => await GetLogListAsync(ViewBag.PageNum + 1);
}