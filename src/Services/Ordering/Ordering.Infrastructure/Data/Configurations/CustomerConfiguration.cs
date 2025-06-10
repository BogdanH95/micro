using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            dbId => CustomerId.Of(dbId));
        
        builder.Property(x=> x.Name).HasMaxLength(100).IsRequired();
        
        builder.Property(x=> x.Email).HasMaxLength(255);
        builder.HasIndex(x=> x.Email).IsUnique();
    }
}