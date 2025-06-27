namespace Micro.Web.Pages;

public class ProductListModel(
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<ProductListModel> logger)
    : PageModel
{
    public IEnumerable<string> CategoryList { get; set; } = [];
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    [BindProperty(SupportsGet = true)] 
    public string SelectedCategory { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string categoryId)
    {
        var response = await catalogService.GetProducts();

        CategoryList = response.Products.SelectMany(x => x.Categories).Distinct();

        if (!string.IsNullOrWhiteSpace(categoryId))
        {
            ProductList = response.Products
                .Where(p => p.Categories.Contains(categoryId));
            SelectedCategory = categoryId;
        }
        else
        {
            ProductList = response.Products;
        }

        return Page();
    }

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
        return RedirectToPage("Cart");
    }
}