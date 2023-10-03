using System.Security.Cryptography;
using Grozn.DAL.Interfaces;
using Grozn.Domain.Entities;
using Grozn.Services.Interfaces;

namespace Grozn.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public IEnumerable<User> GetAllAsync()
    {
        var result = _userRepository.SelectAsync();
        return result;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        var candidate = _userRepository.GetAsync(id);
        return candidate; // добавить валидацию
    }

    public Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public void CreateAsync(User user)
    {
        _userRepository.CreateAsync(user);
    }

    public void RemoveAsync(int id)
    {
        _userRepository.DeleteAsync(id);
        if (_userRepository.GetAsync(id) != null) throw new Exception("Remove was failed") ;
    }
}