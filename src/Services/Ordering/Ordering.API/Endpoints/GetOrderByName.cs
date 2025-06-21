using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints;

// public record GetOrdersByNameRequest(string Name);

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrderByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}", async (string name, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(name));

                var response = result.Adapt<GetOrdersByNameResult>();

                return Results.Ok(response);
            })
            .WithName("GetOrderByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Order By Name")
            .WithDescription("Get Order By Name");
    }
}