using Domain.Entities;

namespace Repository.Abstractions;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(int id);
    Task<Course> UpdateAsync(Course t);
    Task CreateAsync(Course t);
    Task DeleteAsync(Course t);
}