using System.Text.Json.Nodes;

namespace Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ArticleBody { get; set; } // todo: хз будет ли сериализоваться такая залупа
    
    public int ThemeId { get; set; }
    public Theme Theme { get; set; }
}