using System.Reflection.Metadata;
using Contracts.Account;
using Domain.Entities;

namespace Service.Abstactions;

public interface IAccountService
{
    Task<AccountDto> GetByIdAsync(int id);
    Task<Account> UpdateAsync(Account t);
    Task<AccountDeletedResponseDto> DeleteAsync(DeleteAccountRequestDto t);
    Task<List<AccountDto>> GetAllAsync();
}