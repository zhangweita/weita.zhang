//using Microsoft.EntityFrameworkCore;

//namespace IPC.DataAccess.Oracle.StrongIdConfig;

//public class StrongIdDbContext : DbContext
//{
//    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
//    {
//        // 配置强类型Id类型
//        configurationBuilder.Properties<ModelId<long>>().HaveConversion<StrongIdConverter<long>>();
//        configurationBuilder.Properties<ModelId<Guid>>().HaveConversion<StrongIdConverter<Guid>>();
//    }
//}

//public class StrongIdConverter : ValueConverter<StrongTypeId>
//{
//    public StrongIdConverter() : base(v => v.Value, v => new(v)) { }
//}

//public record StrongTypeId<T>(T value)
//{
//    public T Value { get; } = value;//主构造函数初始化
//}