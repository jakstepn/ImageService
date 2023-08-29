using IdentityService.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IdentityService.Data;

public class MongoDbClient : IMongoDbClient
{
    private readonly MongoClient _client;

    public MongoDbClient(IOptions<DatabaseConfiguration> options)
    {
        _client = new MongoClient(DatabaseConfiguration.ConnectionString);
    }
    
    private const string DatabaseName = "Users";
    
    public BsonDocument? GetUser(string login, string password)
    {
        throw new NotImplementedException();
    }

    public void CreateUser(string login, string password)
    {
        throw new NotImplementedException();
    }
}