using BuildingBlocks.Identity;
using Micro.Web.Extensions;
using Micro.Web.Refit;

var builder = WebApplication.CreateBuilder(args);
var apiGwUri = builder.Configuration["ApiSettings:GatewayAddress"]!;
// Add services to the container.
builder.Services.AddClientIdentityValidation(builder.Configuration);
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AccessTokenHandler>();

builder.Services.AddRefitClient<ICatalogService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiGwUri))
    .AddHttpMessageHandler<AccessTokenHandler>();
builder.Services.AddRefitClient<IBasketService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiGwUri))
    .AddHttpMessageHandler<AccessTokenHandler>();
builder.Services.AddRefitClient<IOrderingService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiGwUri))
    .AddHttpMessageHandler<AccessTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();