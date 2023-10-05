namespace Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public int TestId { get; set; }
    public Test Test { get; set; }
    
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }
}