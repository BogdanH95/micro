namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrderByNameQuery(string Name) : IQuery<GetOrdersByNameQueryResult>;

public record GetOrdersByNameQueryResult(IEnumerable<OrderDto> Orders);