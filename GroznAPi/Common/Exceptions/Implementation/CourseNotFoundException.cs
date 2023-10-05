namespace Exceptions.Implementation;

public class CourseNotFoundException : NotFoundException
{
    public CourseNotFoundException(string message) : base(message)
    {
    }
}