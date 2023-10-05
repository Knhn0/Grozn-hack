using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> GetRoleAsync(string title)
    {
        var role = await _roleRepository.GetRoleAsync(title);
        if (role == null)
        {
            throw new Exception(message: "Role not found");
        }

        return role;
    }
}