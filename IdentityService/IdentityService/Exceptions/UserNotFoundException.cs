namespace IdentityService.Exceptions;

public class UserNotFoundException : Exception
{
    public string Login { get; }

    public UserNotFoundException(string login)
    {
        Login = login;
    }
}