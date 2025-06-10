using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x=> x.OrderId);
        builder.Property(x => x.OrderId)
            .HasConversion(orderId => orderId.Value,
                id => OrderId.Of(id));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Price).IsRequired();
    }
}