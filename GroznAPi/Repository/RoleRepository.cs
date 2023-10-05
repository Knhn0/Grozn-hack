using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class RoleRepository : IRoleRepository
{
    private readonly Context _dbContext;

    public RoleRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role> GetRoleAsync(string title)
    {
       var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Title == title);
       return role;
    }
}