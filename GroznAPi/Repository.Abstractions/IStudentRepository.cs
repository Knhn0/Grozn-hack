using Domain.Entities;

namespace Repository.Abstractions;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(int id);
    Task<Student> UpdateAsync(Student t);
    Task<Student> CreateAsync(Student t);
    Task<bool> DeleteAsync(Student t);
    Task<UserInfo> GetUserInfoAsync(int id);
    Task<Student> GetByUserIdAsync(int userId);
    Task<List<Course>> GetJoinedCoursesAsync(int id);
    
    Task<bool> IsStudentAsync(int userId);
}