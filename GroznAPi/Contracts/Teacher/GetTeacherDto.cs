using Domain.Entities;

namespace Contracts.Teacher;

public class GetTeacherDto
{
    public int Id { get; set; }

    public UserInfo UserInfo { get; set; }

    public ICollection<Domain.Entities.Course> Courses { get; set; }

}