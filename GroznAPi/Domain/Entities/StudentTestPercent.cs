namespace Domain.Entities;

public class StudentTestPercent
{
    public int Id { get; set; }
    public Test Test { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
    public double Percent { get; set; }
}