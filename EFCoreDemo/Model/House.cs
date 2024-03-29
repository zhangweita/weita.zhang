using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class House
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Owner { get; set; }
}

internal class HouseConfig : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.ToTable("Houses").HasKey(x => x.Id);
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired().IsUnicode().HasComment("房子名称");
        builder.Property(e => e.Owner).HasMaxLength(50).HasComment("房子所有者");
    }
}
