using Domain.Entities;

namespace Service.Abstactions;

public interface IRoleService
{
    Task<Role> GetRoleAsync(string title);
}