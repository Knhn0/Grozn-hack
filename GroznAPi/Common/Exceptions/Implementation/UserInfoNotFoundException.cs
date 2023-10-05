namespace Exceptions.Implementation;

public class UserInfoNotFoundException : NotFoundException
{
    public UserInfoNotFoundException(string message) : base(message)
    {
    }
}