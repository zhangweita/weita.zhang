namespace Users.WebAPI;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class UnitOfWorkAttribute(params Type[] DbContextTypes) : Attribute
{
    public Type[] DbContextTypes { get; init; } = DbContextTypes;
}
