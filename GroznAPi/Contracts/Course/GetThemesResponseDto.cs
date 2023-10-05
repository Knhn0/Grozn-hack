namespace Contracts.Course;

public class GetThemesResponseDto
{
    public class ThemeDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    
    public List<ThemeDto> Themes { get; set; }
}