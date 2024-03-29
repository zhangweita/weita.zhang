using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

//[Table("Books")]
internal class Book
{
    public long Id { get; set; } //主键

    //[Required, MaxLength(50), Comment("标题")]
    public string? Title { get; set; } //标题

    public DateTime PublishTime { get; set; } //发布日期

    public double Price { get; set; } //单价

    //[Required, MaxLength(20), Comment("作者")]
    public string? AuthorName { get; set; }//作者

    public bool IsDeleted { get; set; }
}

internal class BookEntityConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books", t =>
        {
            t.HasComment("书籍信息");
        });
        builder.HasQueryFilter(f => f.IsDeleted == false);
        builder.Ignore(b => b.Price).HasKey(b => b.Id);//.ToView("Book")
        builder.Property(e => e.Title).HasMaxLength(50).IsRequired().HasComment("标题");
        builder.Property(e => e.AuthorName).HasMaxLength(20).IsRequired().HasComment("作者");
        builder.Property(e => e.Price).HasComment("价格");
        builder.Property(e => e.PublishTime).HasComment("发布时间");
    }
}