using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.IdentityServer.Data.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("UserTokens");
        builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        builder.HasOne<User>(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
        builder.Property(x => x.LoginProvider)
            .HasMaxLength(128);
    }
}