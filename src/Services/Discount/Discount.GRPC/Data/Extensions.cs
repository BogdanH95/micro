using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateAsyncScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.MigrateAsync().Wait();
        
        return app; 
    }
}