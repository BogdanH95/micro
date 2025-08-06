using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.IdentityServer.Data.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("RoleClaims");
        builder.HasKey(x => x.Id);
        builder.HasOne<Role>(x => x.Role)
            .WithMany()
            .HasForeignKey(x=>x.RoleId)
            .IsRequired();
        builder.Property(x => x.ClaimType)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.ClaimValue)
            .HasMaxLength(255);
    }
}