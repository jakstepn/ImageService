using MongoDB.Bson;

namespace IdentityService.Data;

public interface IMongoDbClient
{
    BsonDocument? GetUser(string login, string password);
    void CreateUser(string login, string password);
}