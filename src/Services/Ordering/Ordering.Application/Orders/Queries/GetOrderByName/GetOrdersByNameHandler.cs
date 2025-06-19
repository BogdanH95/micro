namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrderByNameQuery, GetOrdersByNameQueryResult>
{
    public async Task<GetOrdersByNameQueryResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        // get orders by name
        var orders = await context.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value == query.Name)
            .ToListAsync(cancellationToken);
        

        return new GetOrdersByNameQueryResult(orders.ToOrderDtoList());
    }

    
}