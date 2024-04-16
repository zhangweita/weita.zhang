using Microsoft.EntityFrameworkCore;

namespace IPC.DataAccess.Sqlite
{
    public class SqliteEFDbContext(DbContextOptions<SqliteEFDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
