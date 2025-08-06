namespace Micro.IdentityServer.Data.Entities;

public class UserLogin : IdentityUserLogin<Guid>
{
    public User? User { get; set; }
}