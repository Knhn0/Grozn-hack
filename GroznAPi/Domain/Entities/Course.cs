namespace Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Teacher Teacher { get; set; }

    public ICollection<Student> Students { get; set; }
    public ICollection<Theme> Themes { get; set; }
}