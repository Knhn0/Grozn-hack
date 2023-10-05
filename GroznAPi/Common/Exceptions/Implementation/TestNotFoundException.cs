namespace Exceptions.Implementation;

public class TestNotFoundException : NotFoundException
{
    public TestNotFoundException(string message) : base(message)
    {
    }
}