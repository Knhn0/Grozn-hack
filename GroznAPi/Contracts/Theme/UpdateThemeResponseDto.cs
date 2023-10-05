namespace Contracts.Theme;

public class UpdateThemeResponseDto
{
    public int ThemeId { get; set; }
    public string Tiile { get; set; }
    public string Description { get; set; }
    public List<Domain.Entities.Lesson> Lessons { get; set; }
}