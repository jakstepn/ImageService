namespace ImageService.Contracts;

public record Image
{
    public string Id { get; init; } = null!;
    public string Base64 { get; init; } = null!;
    public string Category { get; init; } = null!;
}