using IdentityService.Models;

namespace IdentityService.Data;

public interface IMongoDbClient
{
    User GetUser(string login, string password);
    void CreateUser(string login, string password);
}