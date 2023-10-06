namespace Exceptions.Implementation;

public class TeacherNotFoundException : NotFoundException
{
    public TeacherNotFoundException(string message) : base(message)
    {
    }
}