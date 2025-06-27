namespace Micro.Web.Pages;

public class CheckoutModel(IBasketService basketService, ICatalogService catalogService, ILogger<CheckoutModel> logger)
    : PageModel
{
    [BindProperty] public BasketCheckoutModel Order { get; set; } = new();
    public ShoppingCartModel Cart { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket();

        return Page();
    }

    public async Task<IActionResult> OnPostCheckoutAsync()
    {
        logger.LogInformation("Checkout button clicked");

        Cart = await basketService.LoadUserBasket();
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
        Order.UserName = Cart.Username;
        Order.TotalPrice = Cart.TotalPrice;

        await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
        
        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}