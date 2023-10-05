namespace Contracts.Theme;

public class GetThemeResponseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Domain.Entities.Lesson> Lessons { get; set; }
}