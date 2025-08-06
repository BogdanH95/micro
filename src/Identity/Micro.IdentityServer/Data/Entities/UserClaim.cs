namespace Micro.IdentityServer.Data.Entities;

public class UserClaim : IdentityUserClaim<Guid>
{
    public User? User {get; set;}
}