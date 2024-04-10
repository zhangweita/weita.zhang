using IPC.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace IPC.DataAccess.Oracle;

public class OracleEFDbContext : DbContext
{
    private string connectionString;
    public OracleEFDbContext(string connectionString)
    {
        this.connectionString = connectionString ?? throw new Exception("数据库连接不能为空！");
    }

    public DbSet<API_LOG> ApiLogs { get; set; }
    public DbSet<EQUIPMENT> Equipments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(connectionString);
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
