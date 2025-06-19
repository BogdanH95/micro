namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerQueryResult>;

public record GetOrdersByCustomerQueryResult(IEnumerable<OrderDto> Orders);