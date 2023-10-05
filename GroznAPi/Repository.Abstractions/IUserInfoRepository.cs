using Domain.Entities;

namespace Repository.Abstractions;

public interface IUserInfoRepository
{
    Task<List<UserInfo>> GetAllAsync();
    Task<UserInfo> GetByIdAsync(int id);
    Task<UserInfo> UpdateAsync(UserInfo t);
    Task<UserInfo> CreateAsync(UserInfo t);
    Task<bool> DeleteAsync(UserInfo t);
}