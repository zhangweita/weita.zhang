using IPC.Web.Models;

namespace IPC.Web.Common.ClassTypes;

public interface IDbContextFactory
{
    public OracleDbContext CreateContext(DatabaseType database);
}
