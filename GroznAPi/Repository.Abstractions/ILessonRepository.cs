using Domain.Entities;

namespace Repository.Abstractions;

public interface ILessonRepository
{
    Task<List<Lesson>> GetAllAsync();
    Task<Lesson> GetByIdAsync(int id);
    Task<Lesson> UpdateAsync(Lesson t);
    Task<Lesson> CreateAsync(Lesson t);
    Task<Lesson> DeleteAsync(Lesson t);
    
}