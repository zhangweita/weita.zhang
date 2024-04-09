using IPC.Web.Models;

namespace IPC.Web.ViewModels.ApiLog;

public class ApiLogPaginationInfo
{
    public int PageSize { get; set; }
    public int CurrentPageNum { get; set; }
    public List<API_LOG> LogList { get; set; }
}
