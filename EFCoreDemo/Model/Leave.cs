using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.Model;

internal class Leave
{
    public int Id { get; set; }
    public User Requester { get; set; }
    public User? Approver { get; set; }
    public string Remarks { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int Status { get; set; }
}


internal class LeaveConfig : IEntityTypeConfiguration<Leave>
{
    void IEntityTypeConfiguration<Leave>.Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.ToTable("Leaves");
        builder.HasOne(l => l.Requester).WithMany();
        builder.HasOne(l => l.Approver).WithMany();
        builder.Property(l => l.Remarks).HasMaxLength(1000).IsUnicode();
    }
}