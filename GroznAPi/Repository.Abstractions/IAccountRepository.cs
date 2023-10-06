using Domain.Entities;

namespace Repository.Abstractions;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task<Account> UpdateAsync(Account t);
    Task<Account> CreateAsync(Account t);
    Task<bool> DeleteAsync(Account t);
}