namespace IdentityService.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public string Login { get; }

    public UserAlreadyExistsException(string login)
    {
        Login = login;
    }
}