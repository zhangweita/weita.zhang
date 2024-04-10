using IPC.Web.Models;

namespace IPC.Presentation.Web.Common.ClassTypes;
public class DbContextFactory : IDbContextFactory
{
    private IConfiguration Cfg { get; set; }
    public DbContextFactory(IConfiguration configuration)
    {
        Cfg = configuration;
    }

    public OracleDbContext CreateContext(DatabaseType database)
    {
        string connectionString = database switch
        {
            DatabaseType.OracleFormal => Cfg.GetConnectionString("OracleFormalDbContextConnection"),
            DatabaseType.OracleTest => Cfg.GetConnectionString("OracleTestDbContextConnection"),
            _ => throw new Exception("数据库不存在！")
        };
        connectionString = DesEncryption.Decrypt(connectionString);
        return new(connectionString);
    }
}
