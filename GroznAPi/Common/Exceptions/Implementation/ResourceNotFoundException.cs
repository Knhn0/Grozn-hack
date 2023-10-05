namespace Exceptions.Implementation;

public class ResourceNotFoundException : NotFoundException
{
    public ResourceNotFoundException(string message) : base(message)
    {
    }
}