namespace Domain.Entities;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Uri Url { get; set; }
}