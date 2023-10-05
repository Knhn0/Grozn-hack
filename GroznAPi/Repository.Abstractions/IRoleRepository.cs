using Domain.Entities;
using Presistence;

namespace Repository.Abstractions;

public interface IRoleRepository
{
    Task<Role> GetRoleAsync(string title);
}