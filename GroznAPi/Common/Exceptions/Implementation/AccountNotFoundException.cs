namespace Exceptions.Implementation;

public class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException(string message) : base(message)
    {
    }
}