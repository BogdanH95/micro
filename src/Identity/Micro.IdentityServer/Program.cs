
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//TODO Refactor this section, looks like a bad ideea
//to add extension methods to the builder
builder
    .AddDbContextWithOpenIddict()
    .AddIdentityServices()
    .AddPolicies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

//Register services 
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseMigrations();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Default is 30 days
}
app.UseHttpsRedirection();

app.UseForwardedHeaders();

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();
app.MapControllers();
app.MapDefaultControllerRoute();

await app.SeedData();


app.Run();