using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.IdentityServer.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasKey(r => new { r.UserId, r.RoleId });
        builder.HasOne<User>(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}