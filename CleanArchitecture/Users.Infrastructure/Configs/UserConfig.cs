using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Infrastructure.Configs;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("T_User");

        builder.OwnsOne(x => x.PhoneNumber, nb =>
        {
            nb.Property(x => x.RegionCode).HasMaxLength(5).IsUnicode(false);
            nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
        });

        builder.Property("passwordHash").HasMaxLength(20).IsUnicode(false);
        builder.HasOne(x => x.AccessFail).WithOne(x => x.User).HasForeignKey<UserAccessFail>(x => x.UserId);
    }
}
