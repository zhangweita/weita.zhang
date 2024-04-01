using EFCoreDemo.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace EFCoreDemo;

internal class EFDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<House> Houses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connString = "Data Source=(localdb)\\MSSQLLocalDB;DataBase=EFDemo;Integrated Security=True;";
        optionsBuilder.UseSqlServer(connString);
        optionsBuilder.LogTo(msg =>
        {
            Debug.WriteLine(msg);
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            //using StreamWriter stream = new StreamWriter(path, true, Encoding.Default);
            //stream.Write(DateTime.Now.ToString() + ":" + msg);
            //stream.Write("\r\n");
            //stream.Flush();
            //stream.Close();
        });
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        //modelBuilder.Entity<Book>().ToView("Book").Ignore(b => b.Price).HasKey(b => b.Id);
        modelBuilder.Entity<Book>().HasIndex(b => b.AuthorName);                    // 索引
        modelBuilder.Entity<Book>().HasIndex(b => new { b.Title, b.PublishTime });  // 复合索引
        //modelBuilder.Entity<Book>().Property(b => b.AuthorName).HasColumnName("Author").HasColumnType("varchar(20)");//设置列
    }
}
