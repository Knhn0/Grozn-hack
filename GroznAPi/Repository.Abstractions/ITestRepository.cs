using Domain.Entities;

namespace Repository.Abstractions;

public interface ITestRepository
{
    Task<List<Test>> GetAllAsync();
    Task<Test> GetByIdAsync(int id);
    Task<Test> UpdateAsync(Test t);
    Task<Test> CreateAsync(Test t);
    Task<bool> DeleteAsync(Test t);
}