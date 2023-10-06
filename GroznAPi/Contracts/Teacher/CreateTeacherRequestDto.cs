using Domain.Entities;

namespace Contracts.Teacher;

public class CreateTeacherRequestDto
{
    public int Id { get; set; }
    public UserInfo UserInfo { get; set; }
    
}