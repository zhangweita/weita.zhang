using IPC.DataAccess.Oracle.Factory;
using IPC.Model.Entity;
using Microsoft.Extensions.Configuration;

namespace IPC.DataAccess.ApiLog;

public class API_LOG_DAL : BaseDAL<API_LOG>
{
    public API_LOG_DAL(IDbContextFactory DbFactory, IConfiguration configuration) : base(DbFactory, configuration)
    {
    }
}
