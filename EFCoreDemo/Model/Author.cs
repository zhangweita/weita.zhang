using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class Author
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

}

internal class AuthorEntityConfig : IEntityTypeConfiguration<Author>
{
    void IEntityTypeConfiguration<Author>.Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors", t =>
        {
            t.HasComment("作者信息");
        }).HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(20).IsRequired().HasComment("作者");
    }
}
