namespace Contracts.Course;

public class CreateCourseRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int TeacherId { get; set; }
}