using IdentityService.Models;
using MongoDB.Bson;

namespace IdentityService.Data;

public interface IDatabaseProvider
{
    public User GetUser(string login, string password);
    public void CreateUser(User user);
}