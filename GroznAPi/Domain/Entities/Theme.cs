namespace Domain.Entities;

public class Theme
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Course Course { get; set; }

    public List<Lesson> Lessons { get; set; }
}