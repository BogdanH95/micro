namespace Micro.Web.Models.Basket;

public class ShoppingCartModel
{
    public string Username { get; set; } = string.Empty;
    public List<ShoppingCartItemModel> Items { get; set; } = [];
    public decimal TotalPrice { get; set; }
}

public class ShoppingCartItemModel
{
    public int Quantity { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
}

//Wrapper classes
public record GetBasketResponse(ShoppingCartModel Cart);

public record StoreBasketRequest(ShoppingCartModel Cart);

public record StoreBasketResponse(string Username);

public record DeleteBasketResponse(bool IsSuccess);