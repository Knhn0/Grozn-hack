using System.Text.Json.Nodes;

namespace Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; }
    public int ThemeId { get; set; }
    public ICollection<Test> Tests { get; set; }
}