
using IPC.Web.Common.ClassTypes;
using IPC.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace IPC.Web.Services
{
    public class ApiLogService : BaseService
    {
        private readonly IDbContextFactory _dbContextFactory;

        public ApiLogService(IDbContextFactory dbContextFactory, ILogger<ApiLogService> logger, IConfiguration cfg) : base(logger, cfg)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public async Task<List<API_LOG>> GetLogListAsync(int pageNum, int pageSize)
        {
            using OracleDbContext db = _dbContextFactory.CreateContext(DatabaseType.OracleTest);

            int recordCount = await db.ApiLogs.CountAsync();
            int pageCount = (int)Math.Ceiling(recordCount * 1.0 / pageSize);

            return await db.ApiLogs.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
