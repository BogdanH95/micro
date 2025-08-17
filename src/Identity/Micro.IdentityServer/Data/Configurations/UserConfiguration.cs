using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.IdentityServer.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).IsRequired()
            .HasMaxLength(255);
        builder.Property(x=> x.Email).IsRequired()
            .HasMaxLength(128);
        builder.Property(x => x.FirstName).IsRequired()
            .HasMaxLength(128);
        builder.Property(x => x.LastName).IsRequired()
            .HasMaxLength(128);
        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        builder.Property(x => x.AccessType)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.HasOne<Role>(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}