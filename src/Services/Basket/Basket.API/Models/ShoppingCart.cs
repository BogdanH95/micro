using Marten.Schema;

namespace Basket.API.Models;

public class ShoppingCart
{
    [Identity]
    public string Username { get; set; } = null!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal Total => Items.Sum(i => i.Price * i.Quantity);

    public ShoppingCart(string username)
    {
        Username = username;
    }

//Required for Mapping
    public ShoppingCart()
    {
    }
}