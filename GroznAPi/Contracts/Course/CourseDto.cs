using Contracts.Theme;

namespace Contracts.Course;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    //  public TeacherDto Teacher { get; set; }

    //   public ICollection<Student> Students { get; set; }
    public List<ThemeDto> Themes { get; set; }
}