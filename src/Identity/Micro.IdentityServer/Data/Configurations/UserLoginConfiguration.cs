using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.IdentityServer.Data.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("UserLogins");
        builder.HasKey(x=> new { x.UserId, x.LoginProvider, x.ProviderKey });
        builder.HasOne<User>(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.LoginProvider)
            .HasMaxLength(128);
    }
}