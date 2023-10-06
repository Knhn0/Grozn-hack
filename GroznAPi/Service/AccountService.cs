using System.ComponentModel;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)

    {
        _accountRepository = accountRepository;
    }

    public async Task CreateAsync(Account t)
    {
        await _accountRepository.CreateAsync(t);
    }

    public async Task<Account> UpdateAsync(Account t)
    {
        var candidate = await _accountRepository.GetByIdAsync(t.Id);
        if (candidate == null) throw new Exception("User not found");
        //if (!String.IsNullOrEmpty(t.Email)) candidate.Email = t.Email;
        if (!String.IsNullOrEmpty(t.Username)) candidate.Username = t.Username;
        if (!String.IsNullOrEmpty(t.Password)) candidate.Password = t.Password; // need add hasher
        //if (!String.IsNullOrEmpty(t.Phone)) candidate.Phone = t.Password;
        await _accountRepository.UpdateAsync(candidate);
        return candidate;
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _accountRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(Account t)
    {
        var candidate = await _accountRepository.GetByIdAsync(t.Id);
        if (candidate == null)
        {
            throw new Exception("User not found");
        }
        await _accountRepository.DeleteAsync(t);
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _accountRepository.GetAllAsync();
    }
}