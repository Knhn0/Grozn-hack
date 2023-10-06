using Domain.Entities;

namespace Repository.Abstractions;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllAsync();
    Task<Question> GetByIdAsync(int id);
    Task<Question> UpdateAsync(Question t);
    Task<Question> CreateAsync(Question t);
    Task<bool> DeleteAsync(Question t);
}