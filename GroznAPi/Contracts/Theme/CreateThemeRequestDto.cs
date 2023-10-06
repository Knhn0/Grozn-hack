using System.ComponentModel.DataAnnotations;
using Contracts.Lesson;
using Newtonsoft.Json;

namespace Contracts.Theme;

public class CreateThemeRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
    public List<LessonDto> Lessons { get; set; }
}