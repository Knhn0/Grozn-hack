namespace Contracts.Test;

public class QuestionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<AnswerDto> Answers { get; set; }
    public int TestId { get; set; }
}