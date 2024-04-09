using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IPC.Web.Data;

public class SqliteDbContext : IdentityDbContext<IdentityUser>
{
    private IConfiguration _configuration;
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("SqliteDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SqliteDbContextConnection' not found.");
        optionsBuilder.UseSqlite(connectionString);
        //base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);


    }
}
