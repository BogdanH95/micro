namespace Micro.IdentityServer.Data.Entities;

public class UserToken :  IdentityUserToken<Guid>
{
    public User? User { get; set; }
}