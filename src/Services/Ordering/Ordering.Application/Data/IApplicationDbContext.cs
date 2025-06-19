using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Order> Orders { get; }
    DbSet<Product> Products { get; }
    DbSet<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}