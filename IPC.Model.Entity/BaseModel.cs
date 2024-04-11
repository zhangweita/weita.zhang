using System.Net;

namespace IPC.Model.Entity;

public abstract class BaseModel<TId> where TId : struct
{
    public ModelId<TId> Id { get; set; }
}

public readonly struct ModelId<TId> where TId : struct
{
    public ModelId(TId value) => Value = value;
    public TId Value { get; }
}
