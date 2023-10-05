using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Contracts.Theme;

public class CreateThemeRequestDto
{
    public int ThemeId { get; set; }
    public string Tiile { get; set; }
    public string Description { get; set; }
    public List<Domain.Entities.Lesson> Lessons { get; set; }
}