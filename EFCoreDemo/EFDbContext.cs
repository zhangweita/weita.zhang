using EFCoreDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo;

internal class EFDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connString = "Data Source=(localdb)\\MSSQLLocalDB;DataBase=EFDemo;Integrated Security=True;";
        optionsBuilder.UseSqlServer(connString);
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        modelBuilder.Entity<Book>().ToView("Book").Ignore(b => b.Price).HasKey(b => b.Id);
        modelBuilder.Entity<Book>().HasIndex(b => b.AuthorName);                    // 索引
        modelBuilder.Entity<Book>().HasIndex(b => new { b.Title, b.PublishTime });  // 复合索引
        modelBuilder.Entity<Book>().Property(b => b.AuthorName).HasColumnName("Author").HasColumnType("varchar(20)");//设置列
    }
}
