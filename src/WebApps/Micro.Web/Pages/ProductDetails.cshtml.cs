namespace Micro.Web.Pages;

public class ProductDetailsModel(
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<ProductDetailsModel> logger)
    : PageModel
{
    public ProductModel Product { get; set; } = new();

    [BindProperty] public string Color { get; set; } = string.Empty;
    [BindProperty] public int Quantity { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        var response = await catalogService.GetProductById(productId);
        Product = response.Product;

        return Page();
    }
//TODO: Improve. This method is duplicated 3 times. 
    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");
        var productResponse = await catalogService.GetProductById(productId);

        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            Quantity = 1,
            Color = "Black",
            Price = productResponse.Product.Price,
            ProductName = productResponse.Product.Name
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));
        return RedirectToPage();
    }
}