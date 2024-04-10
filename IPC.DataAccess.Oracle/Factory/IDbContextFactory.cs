namespace IPC.DataAccess.Oracle.Factory;

public interface IDbContextFactory
{
    public OracleEFDbContext CreateContext(DatabaseType database);
}
