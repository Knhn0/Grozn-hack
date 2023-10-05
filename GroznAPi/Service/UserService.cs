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

    public async Task CreateAsync(User t)
    {
        await _userRepository.CreateAsync(t);
    }

    public async Task<User> UpdateAsync(User t)
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

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(User t)
    {
        var candidate = await _userRepository.GetByIdAsync(t.Id);
        if (candidate == null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteAsync(t);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}