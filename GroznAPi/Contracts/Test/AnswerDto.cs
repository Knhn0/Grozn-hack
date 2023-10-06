namespace Contracts.Test;

public class AnswerDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsRight { get; set; }
    public int QuestionId { get; set; }
}