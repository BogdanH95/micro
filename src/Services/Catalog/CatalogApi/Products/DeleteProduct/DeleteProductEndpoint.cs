namespace CatalogAPI.Products.DeleteProduct;

//publid record DeleteProductRequest(Guid Id);
public record DeleteProductResponse(bool IsSuccess, string ErrorMessage);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(Id));
            var response = result.Adapt<DeleteProductResponse>();

            if (response.IsSuccess)
                return Results.Ok();
            else
                return Results.NotFound(new { response.ErrorMessage });
        })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}
