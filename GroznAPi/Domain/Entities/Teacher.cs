namespace Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public UserInfo User { get; set; }
    
    public ICollection<Course> Courses { get; set; }
}