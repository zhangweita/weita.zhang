using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPC.Web.Models;

public class EQUIPMENT
{
    /// <summary>
    /// 设备编码
    /// </summary>
    public string EQUIPMENTCODE { get; set; }
    /// <summary>
    /// 设别名称
    /// </summary>
    public string EQUIPMENTNAMEZH { get; set; }
    /// <summary>
    /// 资源编码
    /// </summary>
    public string RESOURCECODE { get; set; }
    /// <summary>
    /// 工序
    /// </summary>
    public string PROCESS { get; set; }
    /// <summary>
    /// 线体
    /// </summary>
    public string LINE { get; set; }
    /// <summary>
    /// 工作中心
    /// </summary>
    public string WORKCENTER { get; set; }
}

public class EquipmentConfig : IEntityTypeConfiguration<EQUIPMENT>
{
    public void Configure(EntityTypeBuilder<EQUIPMENT> builder)
    {
        builder.HasNoKey();
    }
}
