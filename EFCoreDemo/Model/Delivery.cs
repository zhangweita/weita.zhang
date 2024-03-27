using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace EFCoreDemo.Model;

internal class Delivery
{
    public long Id { get; set; }
    public string CompanyName { get; set; } //快递公司名
    public string Number { get; set; } //快递单号
    [JsonIgnore]
    public Order Order { get; set; } //订单
    public long OrderId { get; set; } //指向订单的外键
}

internal class DeliveryConfig : IEntityTypeConfiguration<Delivery>
{
    void IEntityTypeConfiguration<Delivery>.Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Deliveries");
        builder.Property(d => d.CompanyName).IsUnicode().HasMaxLength(10);
        builder.Property(d => d.Number).IsUnicode().HasMaxLength(50);
    }
}
