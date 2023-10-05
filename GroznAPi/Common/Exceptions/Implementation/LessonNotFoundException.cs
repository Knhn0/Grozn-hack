namespace Exceptions.Implementation;

public class LessonNotFoundException : NotFoundException
{
    public LessonNotFoundException(string message) : base(message)
    {
    }
}