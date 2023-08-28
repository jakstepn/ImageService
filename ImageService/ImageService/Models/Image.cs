using MongoDB.Bson.Serialization.Attributes;

namespace ImageService.Models;

public record Image
{
    public string Id { get; init; } = null!;
    
    [BsonElement("base64")]
    public string Base64 { get; init; } = null!;
    
    [BsonElement("category")]
    public string Category { get; init; } = null!;
    
    [BsonElement("owner_id")]
    public string OwnerId { get; init; } = null!;
}