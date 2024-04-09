using Microsoft.EntityFrameworkCore;

namespace IPC.Web.Models;

public class OracleDbContext : DbContext
{
    private string connectionString;
    public OracleDbContext(string connectionString)
    {
        this.connectionString = connectionString ?? throw new Exception("数据库连接不能为空！");
    }

    public DbSet<API_LOG> ApiLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(connectionString);
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
