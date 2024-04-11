using IPC.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IPC.DataAccess.Oracle.StrongIdConfig;

internal class StrongIdDbContext : DbContext
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<ModelId>().HaveConversion<AuthorIdConverter>();
    }


}
private class StrongIdConverter<TId> : ValueConverter<ModelId<TId>, long>
{
    public StrongIdConverter()
        : base(v => v.Value, v => new(v))
    {
    }
}
