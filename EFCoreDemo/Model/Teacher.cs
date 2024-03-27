using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students = new();
}

internal class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    void IEntityTypeConfiguration<Teacher>.Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.Property(s => s.Name).HasMaxLength(20).IsUnicode();
    }
}
