namespace Contracts.Theme;

public class CreateThemeResponseDto
{
    public int ThemeId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Domain.Entities.Lesson> Lessons { get; set; }
}