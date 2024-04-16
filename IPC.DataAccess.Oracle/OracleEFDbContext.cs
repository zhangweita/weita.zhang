using IPC.Model.Entity;

namespace IPC.DataAccess.Oracle;

public class OracleEFDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<API_LOG> ApiLogs { get; set; }
    public DbSet<EQUIPMENT> Equipments { get; set; }
    public DbSet<ACTIONNAME> ActionNames { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}

public class FormalReadDbContext(DbContextOptions<FormalReadDbContext> options) : OracleEFDbContext(options) { }
public class FormalWriteDbContext(DbContextOptions<FormalWriteDbContext> options) : OracleEFDbContext(options) { }
public class TestRWDbContext(DbContextOptions<TestRWDbContext> options) : OracleEFDbContext(options) { }
