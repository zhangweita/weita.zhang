using IPC.DataAccess;
using IPC.DataAccess.Oracle;
using IPC.DataAccess.Oracle.Factory;
using IPC.Model.Entity;
using IPC.Model.ViewModel.ApiLog;

namespace IPC.Service.ApiLog;

public class ApiLogService : BaseService<API_LOG>
{
    public ApiLogService(ILogger<ApiLogService> logger, IConfiguration cfg) : base(logger, cfg)
    {
    }

    public async Task<ApiLogPaginationInfo> GetLogListAsync(int pageNum, int pageSize)
    {

        int recordCount = Dal.;
        var logList = await db.ApiLogs.OrderByDescending(l => l.REQUEST_TIME).Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
        ApiLogPaginationInfo info = new()
        {
            RecordCount = recordCount,
            PageCount = (int)Math.Ceiling(recordCount * 1.0 / pageSize),
            QueryLogList = logList
        };
        return info;
    }
}
