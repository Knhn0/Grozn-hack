namespace Contracts.Test;

public class UpdateTestRequestDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<QuestionDto> Questions { get; set; }
    public int LessonId { get; set; }
}