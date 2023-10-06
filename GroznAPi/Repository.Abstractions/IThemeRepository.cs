using Contracts.Theme;
using Domain.Entities;

namespace Repository.Abstractions;

public interface IThemeRepository
{
    Task<List<Lesson>> GetLessonsAsync();
    Task<List<Theme>> GetAllAsync();
    Task<Theme?> GetByIdAsync(int id);
    Task<Theme> UpdateAsync(Theme t);
    Task<Theme> CreateAsync(Theme t);
    Task<bool> DeleteAsync(int id);
    Task<List<Theme>> GetByCourseId(int courseId);
}