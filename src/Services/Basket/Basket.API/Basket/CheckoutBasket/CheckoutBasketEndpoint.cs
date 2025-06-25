namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkout", (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutBasketCommand>();

            var result = sender.Send(command);

            var response = result.Adapt<CheckoutBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout basket")
        .WithDescription("Checkout basket");
    }
}