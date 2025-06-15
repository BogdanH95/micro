using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(id => id.Value,
                id => OrderId.Of(id));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(o => o.OrderName,
            nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });
        
        builder.ComplexProperty(o => o.ShippingAddress,
            addressBuilder =>
            {
                addressBuilder.Property(n => n.AddressLine)
                    .HasMaxLength(200)
                    .IsRequired();
                
                addressBuilder.Property(n => n.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.EmailAddress)
                    .HasMaxLength(255)
                    .IsRequired();
                
                addressBuilder.Property(n => n.Country)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.State)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.ZipCode)
                    .HasMaxLength(10)
                    .IsRequired();
            });
        
        builder.ComplexProperty(o => o.BillingAddress,
            addressBuilder =>
            {
                addressBuilder.Property(n => n.AddressLine)
                    .HasMaxLength(200)
                    .IsRequired();
                
                addressBuilder.Property(n => n.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.EmailAddress)
                    .HasMaxLength(255)
                    .IsRequired();
                
                addressBuilder.Property(n => n.Country)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.State)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.ZipCode)
                    .HasMaxLength(10)
                    .IsRequired();
            });
        
        builder.ComplexProperty(o => o.Payment,
            paymentBuilder =>
            {
                paymentBuilder.Property(n => n.CardName)
                    .HasMaxLength(100)
                    .IsRequired();
                
                paymentBuilder.Property(n => n.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();
                
                paymentBuilder.Property(n => n.Expiration)
                    .HasMaxLength(10)
                    .IsRequired();
                
                paymentBuilder.Property(n => n.Cvv)
                    .HasMaxLength(3)
                    .IsRequired();

                paymentBuilder.Property(n => n.PaymentMethod);
            });
        
        builder.Property(o=> o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(s=> s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
        
        builder.Property(o => o.TotalPrice);


    }
}