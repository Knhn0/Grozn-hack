using System.ComponentModel;
using Contracts.Account;
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

    public async Task<AccountDto> GetByIdAsync(int id)
    {
        var candidate = await _accountRepository.GetByIdAsync(id);
        var response = new AccountDto
        {
            Id = candidate.Id,
            Username = candidate.Username,
            UserInfo = new UserInfoDto
            {
                Id = candidate.UserInfo.Id,
                FirstName = candidate.UserInfo.FirstName,
                SecondName = candidate.UserInfo.SecondName,
                ThirdName = candidate.UserInfo.ThirdName,
                Role = new RoleDto
                {
                    Id = candidate.UserInfo.Role.Id,
                    Title = candidate.UserInfo.Role.Title
                }
            }
        };
        return response;
    }

    public async Task<AccountDeletedResponseDto> DeleteAsync(DeleteAccountRequestDto request)
    {
        var candidate = await _accountRepository.GetByIdAsync(request.Id);
        if (candidate == null)
        {
            throw new Exception("User not found");
        }
        var upd = await _accountRepository.DeleteAsync(candidate);
        return new AccountDeletedResponseDto
        {
            Deleted = upd,
            Id = candidate.Id
        };
    }

    public async Task<List<AccountDto>> GetAllAsync()
    {
        var candidates = await _accountRepository.GetAllAsync();
        var result = candidates.Select(candidate => new AccountDto
        {
            Id = candidate.Id,
            Username = candidate.Username,
            UserInfo = new UserInfoDto
            {
                Id = candidate.UserInfo.Id,
                FirstName = candidate.UserInfo.FirstName,
                SecondName = candidate.UserInfo.SecondName,
                ThirdName = candidate.UserInfo.ThirdName,
                Role = new RoleDto
                {
                    Id = candidate.UserInfo.Role.Id,
                    Title = candidate.UserInfo.Role.Title
                }
            }
        });
        return result.ToList();
    }
}