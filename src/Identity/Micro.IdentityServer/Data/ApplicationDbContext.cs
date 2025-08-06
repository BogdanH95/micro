using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Micro.IdentityServer.Data;

public class
    ApplicationDbContext
    (DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}