namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        // get orders by name
        var orders = await context.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value == query.Name)
            .OrderBy(o=> o.OrderName.Value)
            .ToListAsync(cancellationToken);
        

        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }

    
}