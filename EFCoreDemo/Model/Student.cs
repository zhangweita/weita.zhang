using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Teacher> Teachers = new();
}

internal class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.Property(s => s.Name).HasMaxLength(20).IsUnicode();
        builder.HasMany(s => s.Teachers).WithMany(t => t.Students).UsingEntity(j => j.ToTable("T_Students_Teachers"));
    }
}
