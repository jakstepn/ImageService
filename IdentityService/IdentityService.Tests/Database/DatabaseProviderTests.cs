using Amazon.Runtime.Internal.Util;
using AutoFixture;
using IdentityService.Data;
using IdentityService.Exceptions;
using IdentityService.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;

namespace IdentityService.Tests.Database;

[TestFixture]
public class DatabaseProviderTests
{
    private Mock<IMongoDbClient> _data;
    private Mock<ILogger<IDatabaseProvider>> _logger;
    private Fixture _fixture;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();

        _data = new Mock<IMongoDbClient>();
        _logger = new Mock<ILogger<IDatabaseProvider>>();

        _data.Setup(d => d.CreateUser(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Test]
    public void UserNotFoundTest()
    {
        _data.Setup(d => d.GetUser(It.IsAny<string>(), It.IsAny<string>()));
        var dbProvider = new DatabaseProvider(_data.Object, _logger.Object);

        Assert.Throws<UserNotFoundException>(() =>
            dbProvider.GetUser(_fixture.Create<string>(), _fixture.Create<string>()));
    }

    [Test]
    public void UserFoundTest()
    {
        var user = new User()
        {
            Login = "Test",
            Password = "Test"
        };
        
        _data.Setup(d => d.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(
            () => new BsonDocument
            {
                { "Login", user.Login },
                { "Password", user.Password }
            });
        var dbProvider = new DatabaseProvider(_data.Object, _logger.Object);

        Assert.That(dbProvider.GetUser(_fixture.Create<string>(), _fixture.Create<string>()), Is.EqualTo(user));
    }

    [Test]
    public void CreateUserThatExistsTest()
    {
        var user = new User()
        {
            Login = "Test",
            Password = "Test"
        };

        _data.Setup(d => d.GetUser(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(() => new BsonDocument
            {
                {
                    "Login", user.Login
                },
                {
                    "Password", user.Password
                }
            });
        _data.Setup(d => d.CreateUser(It.IsAny<string>(), It.IsAny<string>()));
        var dbProvider = new DatabaseProvider(_data.Object, _logger.Object);

        Assert.Throws<UserAlreadyExistsException>(() =>
            dbProvider.CreateUser(user));
    }

    [Test]
    public void CreateUserTest()
    {
        var user = new User()
        {
            Login = It.IsAny<string>(),
            Password = It.IsAny<string>()
        };

        var users = new List<User>();

        _data.Setup(d => d.GetUser(user.Login, user.Password))
            .Returns(() => null);
        _data.Setup(d => d.CreateUser(user.Login, user.Password))
            .Callback<string, string>((login, password) => users.Add(new User
            {
                Login = login,
                Password = password
            }));

        var dbProvider = new DatabaseProvider(_data.Object, _logger.Object);

        dbProvider.CreateUser(user);

        Assert.Multiple(() =>
        {
            Assert.That(users, Has.Count.EqualTo(1));
            Assert.That(users, Has.ItemAt(0).EqualTo(user));
        });
    }
}