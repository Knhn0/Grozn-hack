using System.ComponentModel;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateAsync(Account t)
    {
        await _userRepository.CreateAsync(t);
    }

    public async Task<Account> UpdateAsync(Account t)
    {
        var candidate = await _userRepository.GetByIdAsync(t.Id);
        if (candidate == null) throw new Exception("User not found");
        if (!String.IsNullOrEmpty(t.Email)) candidate.Email = t.Email;
        if (!String.IsNullOrEmpty(t.Username)) candidate.Username = t.Username;
        if (!String.IsNullOrEmpty(t.Password)) candidate.Password = t.Password; // need add hasher
        if (!String.IsNullOrEmpty(t.Phone)) candidate.Phone = t.Password;
        await _userRepository.UpdateAsync(candidate);
        return candidate;
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(Account t)
    {
        var candidate = await _userRepository.GetByIdAsync(t.Id);
        if (candidate == null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteAsync(t);
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}