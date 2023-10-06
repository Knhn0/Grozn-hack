namespace Exceptions.Implementation;

public class ThemeNotFoundException : NotFoundException
{
    public ThemeNotFoundException(string message) : base(message)
    {
    }
}