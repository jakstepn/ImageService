using ImageService.Exceptions;
using ImageService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImageService.Data;

public class MongoDbClient : IMongoDbClient
{
    private readonly MongoClient _client =
        new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));

    private const string DatabaseName = "Images";

    public IEnumerable<Image> GetImages(string collectionName, string ownerId)
    {
        var collection = _client.GetDatabase(DatabaseName).GetCollection<Image>(collectionName).AsQueryable();

        var images = collection.Where(i => i.OwnerId == ownerId);

        return images;
    }

    public void AddImageToCollection(Image image)
    {
        var database = _client.GetDatabase(DatabaseName);
        database.GetCollection<Image>(image.Category).InsertOne(image);
    }

    public void RemoveImageFromCollection(string collectionName, string id, string ownerId)
    {
        var builder = Builders<Image>.Filter;
        var filter = builder.And(
            builder.Eq(i => i.OwnerId, ownerId),
            builder.Eq(i => i.Id, id)
        );
        var database = _client.GetDatabase(DatabaseName);
        database.GetCollection<Image>(collectionName).DeleteOne(filter);
    }

    public IEnumerable<BsonDocument> GetCollections()
    {
        return _client.GetDatabase(DatabaseName).ListCollections().ToEnumerable();
    }

    public void CreateCollection(string collectionName)
    {
        var database = _client.GetDatabase(DatabaseName);
        database.CreateCollection(collectionName);
    }
}