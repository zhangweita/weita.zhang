using IPC.Common.Utils.Encryption;
using Microsoft.Extensions.Configuration;

namespace IPC.DataAccess.Oracle.Factory;
public class DbContextFactory : IDbContextFactory
{
    private IConfiguration Cfg { get; set; }
    public DbContextFactory(IConfiguration configuration)
    {
        Cfg = configuration;
    }

    public OracleEFDbContext CreateContext(DatabaseType database)
    {
        string connectionString = database switch
        {
            DatabaseType.OracleFormal => Cfg.GetConnectionString("OracleFormalDbContextConnection")!,
            DatabaseType.OracleTest => Cfg.GetConnectionString("OracleTestDbContextConnection")!,
            _ => throw new Exception("数据库不存在！")
        };
        connectionString = DesUtil.Decrypt(connectionString);
        return new(connectionString);
    }
}
