namespace Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Resource Resource { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public int TestId { get; set; }
}