using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.IdentityServer.Data.Configurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("UserClaims");
        builder.HasKey(x => x.Id);
        builder.HasOne<User>(x => x.User)
            .WithMany()
            .HasForeignKey(x=> x.UserId)
            .IsRequired(); 
        builder.Property(x => x.ClaimType)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.ClaimValue)
            .HasMaxLength(255);
    }
}