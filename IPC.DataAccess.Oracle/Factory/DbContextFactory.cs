//using IPC.Common.Utils.Encryption;
//using Microsoft.Extensions.Configuration;
//namespace IPC.DataAccess.Oracle.Factory;
//public class DbContextFactory(IConfiguration configuration) : IDbContextFactory
//{
//    private IConfiguration Cfg { get; set; } = configuration;

//    public EFDbContext CreateContext(DatabaseType database)
//    {
//        string connectionString = database switch
//        {
//            DatabaseType.OracleRead => Cfg.GetConnectionString("OracleRead")!,
//            DatabaseType.OracleWrite => Cfg.GetConnectionString("OracleWrite")!,
//            DatabaseType.OracleTest => Cfg.GetConnectionString("OracleTest")!,
//            _ => throw new Exception("数据库不存在！")
//        };
//        connectionString = DesUtil.Decrypt(connectionString);
//        return new(connectionString);
//    }
//}
