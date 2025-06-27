using System.Net;

namespace Micro.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{username}")]
    Task<GetBasketResponse> GetBasket(string username);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{username}")]
    Task<DeleteBasketResponse> DeleteBasket(string username);
    
    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);
    
    public async Task<ShoppingCartModel> LoadUserBasket()
    {
        //Get basket, if not exist create new basket with default logged in user name
        var username = "swn";
        ShoppingCartModel basket;
        try
        {
            var getBasketResponse = await GetBasket(username);
            basket = getBasketResponse.Cart;
        }
        catch (ApiException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            basket = new ShoppingCartModel
            {
                Username = username
            };
        }//TODO: Catch and Handle

        return basket;
    }
}