namespace Contracts.Lesson;

public class DeleteLessonRequestDto
{
    public int LessonId { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; }
    public Domain.Entities.Theme theme { get; set; }
}
