
using IPC.Web.Common.ClassTypes;
using IPC.Web.Models;
using IPC.Web.ViewModels.ApiLog;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace IPC.Web.Services;

public class ApiLogService : BaseService
{
    private readonly IDbContextFactory _dbContextFactory;

    public ApiLogService(IDbContextFactory dbContextFactory, ILogger<ApiLogService> logger, IConfiguration cfg) : base(logger, cfg)
    {
        this._dbContextFactory = dbContextFactory;
    }

    public async Task<ApiLogPaginationInfo> GetLogListAsync(int pageNum, int pageSize)
    {
        using OracleDbContext db = _dbContextFactory.CreateContext(DatabaseType.OracleTest);


        int recordCount = await db.ApiLogs.CountAsync();
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
