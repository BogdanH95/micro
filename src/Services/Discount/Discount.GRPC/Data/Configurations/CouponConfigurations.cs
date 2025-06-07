using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.GRPC.Data.Configurations;

public class CouponConfigurations : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder
            .Property(c => c.ProductName)
            .IsRequired()
            ;
    }
}