using Contracts.Theme;

namespace Contracts.Course;

public class GetThemesResponseDto
{
    public List<ThemeDto> Themes { get; set; }
}