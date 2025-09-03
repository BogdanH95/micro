namespace BuildingBlocks.Identity;

public class IdentityOptions
{
    public const string Identity = "Identity";

    public string Origins { get; init; } = string.Empty!;
    public string Key { get; init; } = string.Empty;
    public string ClientId  { get; init; } = string.Empty;

}