using Contracts.Theme;
using Domain.Entities;

namespace Repository.Abstractions;

public interface IThemeRepository
{
    Task<List<Lesson>> GetLessonsAsync();
    Task<Theme?> GetByIdAsync(int id);
    Task<Theme> UpdateAsync(Theme t);
    Task<Theme> CreateAsync(Theme t);
    Task DeleteAsync(Theme t);
    
}