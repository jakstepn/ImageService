using ImageService.Models;
using MongoDB.Bson;

namespace ImageService.Data;

public interface IMongoDbClient
{
    IEnumerable<BsonDocument> GetCollections();
    void CreateCollection(string collectionName);
    void AddImageToCollection(Image image);
    IEnumerable<Image> GetImages(string category, string ownerId);
    void RemoveImageFromCollection(string collectionName, string id, string ownerId);
}