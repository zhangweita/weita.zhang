namespace IPC.Model.Entity;

public class SCADA_CONFIG
{
    public string ID { get; set; }
    public string MODULE { get; set; }
    public string SUB_MODULE { get; set; }
    public string KEY { get; set; }
    public string VALUE { get; set; }
    public string DESCRIPTION { get; set; }
}
public class ScadaConfigConfig : IEntityTypeConfiguration<SCADA_CONFIG>
{
    public void Configure(EntityTypeBuilder<SCADA_CONFIG> builder)
    {
        builder.HasKey(x => x.ID);
    }
}
