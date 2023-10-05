namespace Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public UserInfo UserInfo { get; set; }
    
    public ICollection<Course> Courses { get; set; }
}