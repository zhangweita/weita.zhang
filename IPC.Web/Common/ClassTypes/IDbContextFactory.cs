using IPC.Web.Models;

namespace IPC.Presentation.Web.Common.ClassTypes;

public interface IDbContextFactory
{
    public OracleDbContext CreateContext(DatabaseType database);
}
