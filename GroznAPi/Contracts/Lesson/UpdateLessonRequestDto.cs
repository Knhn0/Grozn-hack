namespace Contracts.Lesson;

public class UpdateLessonRequestDto
{
    public int LessonId { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; }
    public int ThemeId { get; set; }
}