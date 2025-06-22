using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints;

// public record GetOrdersByCustomerRequest(Guid CustomerId);

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customers/{customerId:guid}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerResult>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Order By Customer")
            .WithDescription("Get Order By Customer");
    }
}