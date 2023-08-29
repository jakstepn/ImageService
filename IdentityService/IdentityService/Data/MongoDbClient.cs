using IdentityService.Configuration;
using IdentityService.Models;
using Microsoft.Extensions.Options;
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
    
    public User GetUser(string login, string password)
    {
        throw new NotImplementedException();
    }

    public void CreateUser(string login, string password)
    {
        throw new NotImplementedException();
    }
}