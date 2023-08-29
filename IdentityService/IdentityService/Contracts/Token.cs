namespace IdentityService.Contracts;

public record Token
{
    public string Value { get; init; } = null!;
    public string Type { get; init; } = null!;
}