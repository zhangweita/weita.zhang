namespace IPC.Model.Entity;

public class ACTIONNAME
{
    public required int ID { get; set; }
    public string ACTION_NAME { get; set; }
}

public class ActionNameConfig : IEntityTypeConfiguration<ACTIONNAME>
{
    public void Configure(EntityTypeBuilder<ACTIONNAME> builder)
    {
        builder.HasKey(x => x.ID);
    }
}
