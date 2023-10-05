namespace Exceptions.Implementation;

public class StudentNotFoundException : NotFoundException
{
    public StudentNotFoundException(string message) : base(message)
    {
    }
}