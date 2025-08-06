namespace Micro.IdentityServer.Data.Entities;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role? Role { get; set; }
}