using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandle(IApplicationDbContext context)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await context.Orders.CountAsync(cancellationToken);

        var orders = await context.Orders
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);


        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageIndex: pageIndex,
                pageSize: pageSize,
                count: totalCount,
                data: orders.ToOrderDtoList()
            ));
    }
}