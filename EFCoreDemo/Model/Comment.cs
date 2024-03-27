using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace EFCoreDemo.Model;

internal class Comment
{
    public long Id { get; set; }
    [JsonIgnore]
    public Article Article { get; set; }
    public string? Message { get; set; }
}

internal class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments").HasOne(c => c.Article).WithMany(a => a.Comments).IsRequired();
        builder.Property(c => c.Message).IsRequired().IsUnicode();
    }
}
