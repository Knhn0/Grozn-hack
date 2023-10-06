namespace Contracts.Teacher;

public class GetCreatedCourseDto
{
    public ICollection<Domain.Entities.Course> GetCourses { get; set; }
}