using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellation = default)
    {
        var cachedBasket = await cache.GetStringAsync(username, token: cancellation);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        var basket = await repository.GetBasket(username, cancellation);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), token: cancellation);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation = default)
    {
        await repository.StoreBasket(basket, cancellation);
        await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = null,
            AbsoluteExpirationRelativeToNow = null,
            SlidingExpiration = new TimeSpan(0,5,0),
        },token: cancellation);
        return basket;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellation = default)
    {
        await repository.DeleteBasket(username, cancellation);
        await cache.RemoveAsync(username, cancellation);
        return true;
    }
}