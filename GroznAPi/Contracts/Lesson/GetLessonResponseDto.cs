namespace Contracts.Lesson;

public class GetLessonResponseDto
{
    public int LessonId { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; }
    public Domain.Entities.Theme theme { get; set; }

}

public class LessonPercentDto
{
    public double Percent { get; set; }
    public string Title { get; set; }
}

public class GetLessonsPercentResponseDto
{
    public List<LessonPercentDto> lessons { get; set; }
}