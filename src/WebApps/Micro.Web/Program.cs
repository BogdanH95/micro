using Micro.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
var apiGwUri = builder.Configuration["ApiSettings:GatewayAddress"]!;
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services
    .AddRefitFor<ICatalogService>(apiGwUri)
    .AddRefitFor<IBasketService>(apiGwUri)
    .AddRefitFor<IOrderingService>(apiGwUri);

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

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();