namespace IPC.Presentation.Web.ViewModels.ApiLog;

/// <summary>
/// 接口日志分页信息
/// </summary>
public class ApiLogPaginationInfo
{
    /// <summary>
    /// 记录数
    /// </summary>
    public int RecordCount { get; set; }
    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; }
    /// <summary>
    /// 单页数据集
    /// </summary>
    public List<API_LOG> QueryLogList { get; set; }
}
