namespace Contracts.Lesson;

public class CreateLessonRequestDto
{
    public int LessonId { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; }
    public int ThemeId { get; set; }
}