using IPC.Model.ViewModel.ApiLog;
using IPC.Service.ApiLog;
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

    public IActionResult Index()
    {
        ViewBag.PageIndex = 1;
        ViewBag.PageSize = 15;
        ViewBag.LineList = new[] { "X1", "X2", "A1", "A2", "A3", "C1", "C2", "C3" };
        ViewBag.EquipmentList = new[] { "X1", "X2", "A1", "A2", "A3", "C1", "C2", "C3" };
        //ApiLogPaginationInfo pageInfo = await _apiLogService.GetLogListAsync(ViewBag.PageIndex, ViewBag.PageSize);
        //ViewBag.RecordCount = pageInfo.RecordCount;
        //ViewBag.PageCount = pageInfo.PageCount;

        return View();
    }

    public async Task<IActionResult> GetPageData(int pageIndex = 1)
    {
        int pageSize = ViewBag.PageSize;
        ApiLogPaginationInfo pageInfo = await _apiLogService.GetLogListAsync(pageIndex, pageSize);

        ViewBag.PageIndex = pageIndex;
        ViewBag.RecordCount = pageInfo.RecordCount;
        ViewBag.PageCount = pageInfo.PageCount;

        return View("Index", pageInfo.QueryLogList);
    }
}