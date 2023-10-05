namespace Domain.Entities;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; }
}