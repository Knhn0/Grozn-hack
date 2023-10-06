namespace Domain.Entities;

public class Answer
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsRight { get; set; }
    
    public int QuestionId { get; set; }
}