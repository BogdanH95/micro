namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string username, CancellationToken cancellation = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation = default);
    Task<bool> DeleteBasket(string username, CancellationToken cancellation = default);
}