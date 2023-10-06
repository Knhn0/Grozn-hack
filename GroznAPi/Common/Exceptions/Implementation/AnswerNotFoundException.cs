namespace Exceptions.Implementation;

public class AnswerNotFoundException : NotFoundException
{
    public AnswerNotFoundException(string message) : base(message)
    {
    }
}