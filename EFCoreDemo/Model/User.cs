using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

internal class UserConfig : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(u => u.Name).IsRequired().HasMaxLength(200).IsUnicode();
    }
}
