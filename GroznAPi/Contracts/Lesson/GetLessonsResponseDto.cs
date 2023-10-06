namespace Contracts.Lesson;

public class GetLessonsResponseDto
{
    public LessonDto Lesson { get; set; }
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