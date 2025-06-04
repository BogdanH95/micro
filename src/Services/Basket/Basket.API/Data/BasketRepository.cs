namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellation = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(username, cancellation)
                     ?? throw new BasketNotFoundException(username);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellation);
        return basket;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellation = default)
    {
        session.Delete<ShoppingCart>(username);
        await session.SaveChangesAsync(cancellation);
        return true;
    }
}