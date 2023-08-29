using IdentityService.Exceptions;
using IdentityService.Models;
using MongoDB.Bson.Serialization;

namespace IdentityService.Data;

public class DatabaseProvider : IDatabaseProvider
{
    private readonly IMongoDbClient _client;
    private readonly ILogger<IDatabaseProvider> _logger;

    public DatabaseProvider(IMongoDbClient client, ILogger<IDatabaseProvider> logger)
    {
        _client = client;
        _logger = logger;
    }

    public User GetUser(string login, string password)
    {
        var document = _client.GetUser(login, password);

        if (document == null)
        {
            throw new UserNotFoundException(login);
        }
        
        var user = BsonSerializer.Deserialize<User>(document);
        
        return user;
    }

    public void CreateUser(User user)
    {
        try
        {
            var _ = GetUser(user.Login, user.Password);
            throw new UserAlreadyExistsException(user.Login);
            _logger.Log(LogLevel.Critical, "User Exists! Login: {login}", user.Login);
        }
        catch (UserNotFoundException e)
        {
            _client.CreateUser(user.Login, user.Password);
        }
    }
}