using Domain.Entities;

namespace Repository.Abstractions;

public interface IAnswerRepository
{
    Task<List<Answer>> GetAllAsync();
    Task<Answer> GetByIdAsync(int id);
    Task<Answer> UpdateAsync(Answer t);
    Task<Answer> CreateAsync(Answer t);
    Task<bool> DeleteAsync(Answer t);
}