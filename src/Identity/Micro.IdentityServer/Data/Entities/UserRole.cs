namespace Micro.IdentityServer.Data.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public User? User { get; set; }
}