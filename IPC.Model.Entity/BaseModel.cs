namespace IPC.Model.Entity;

public abstract class BaseModel
{
    //public required BaseId<TId> Id { get; set; }
}

/// <summary>
/// 模型类ID定义
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <param name="value"></param>
public record IntegerId : BaseId<int>
{
    public IntegerId(int value) : base(value) { }
}

/// <summary>
/// 模型类ID定义
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <param name="value"></param>
public record GuidId : BaseId<Guid>
{
    public GuidId(Guid value) : base(value) { }
}

public record BaseId<T>(T Value)
{
    public T Value { get; } = Value;//主构造函数初始化
}