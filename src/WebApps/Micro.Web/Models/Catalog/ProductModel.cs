// ReSharper disable ClassNeverInstantiated.Global
namespace Micro.Web.Models.Catalog;

public class ProductModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public List<string> Categories { get; init; } = [];
    public string Description { get; init; } = string.Empty;
    public string ImageFile { get; init; } = string.Empty;
    public decimal Price { get; init; } 
}

//wrapper classes
public record GetProductsResponse(IEnumerable<ProductModel> Products);
public record GetProductsByCategoryResponse(IEnumerable<ProductModel> Products);
public record GetProductByIdResponse(ProductModel Product);