namespace Exceptions.Implementation;

public class QuestionNotFoundException : NotFoundException
{
    public QuestionNotFoundException(string message) : base(message)
    {
    }
}