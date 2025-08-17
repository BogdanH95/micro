using Micro.IdentityServer.Data.Entities.Enums;

namespace Micro.IdentityServer.Data.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public Guid RoleId { get; init; }
    public Role Role { get; init; } = new();
    public AccessType AccessType { get; init; } = AccessType.Regular;
    
}