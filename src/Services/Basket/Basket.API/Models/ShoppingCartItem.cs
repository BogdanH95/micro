namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; } = 0;
    public string Color { get; set; } = null!;
    public decimal Price { get; set; } = 0;
    public Guid ProductId { get; set; } = Guid.Empty;
    public string ProductName { get; set; } = null!;
}