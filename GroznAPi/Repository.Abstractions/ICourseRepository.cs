using Domain.Entities;

namespace Repository.Abstractions;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(int id);
    Task<Course> UpdateAsync(Course t);
    Task<Course> CreateAsync(Course t);
    Task<bool> DeleteAsync(Course t);
    Task<Course> AddStudent(int id, Student student);
}