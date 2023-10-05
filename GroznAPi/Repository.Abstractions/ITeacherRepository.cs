using Domain.Entities;

namespace Repository.Abstractions;

public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllAsync();
    Task<Teacher> GetByIdAsync(int id);
    Task<Teacher> UpdateAsync(Teacher t);
    Task<Teacher> CreateAsync(Teacher t);
    Task<bool> DeleteAsync(Teacher t);
    Task<UserInfo> GetUserInfoAsync(int id);
    Task<Teacher> GetByUserIdAsync(int userId);
    Task<List<Course>> GetCreatedCoursesAsync(int id);
    
    Task<bool> IsTeacherAsync(int userId);
}