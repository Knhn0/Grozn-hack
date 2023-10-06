namespace Contracts.Lesson;

public class GetLessonRequestDto
{
    public int LessonId { get; set; }
}

public class GetLessonsPercentRequest
{
    public int ThemeId { get; set; }
    public int StudentId { get; set; }
}