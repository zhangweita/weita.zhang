using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace EFCoreDemo.Model;

internal class Order
{
    public long Id { get; set; }
    public string Name { get; set; } //商品名
    public string Address { get; set; } //收货地址
    [JsonIgnore]
    public Delivery? Delivery { get; set; } //快递信息
}

internal class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasOne(o => o.Delivery).WithOne(d => d.Order).HasForeignKey<Delivery>(d => d.OrderId);
        builder.Property(o => o.Name).IsUnicode();
        builder.Property(o => o.Address).IsUnicode();
    }
}
