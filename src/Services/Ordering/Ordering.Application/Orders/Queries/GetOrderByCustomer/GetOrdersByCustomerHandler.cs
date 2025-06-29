﻿namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(x=> x.OrderItems)
            .AsNoTracking()
            .Where(x=> x.CustomerId == CustomerId.Of(query.CustomerId))
            .OrderBy(o=> o.OrderName.Value)
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}