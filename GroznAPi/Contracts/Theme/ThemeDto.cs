using Contracts.Lesson;

namespace Contracts.Theme;

public class ThemeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
    public List<LessonDto> Lessons { get; set; }
}