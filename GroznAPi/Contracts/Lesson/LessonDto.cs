using Contracts.Test;

namespace Contracts.Lesson;

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; }
    public int ThemeId { get; set; }
    public List<TestDto> Tests { get; set; }
}