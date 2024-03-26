using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class Article
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public List<Comment> Comments = new();
}
internal class ArticleConfig : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");//s.HasMany(a => a.Comments).WithOne();
        builder.Property(a => a.Content).IsRequired().IsUnicode();
        builder.Property(a => a.Title).IsRequired().IsUnicode().HasMaxLength(255);
    }
}
