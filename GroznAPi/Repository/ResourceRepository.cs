using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace Repository;

public class ResourceRepository : IResourceRepository
{
    private readonly Context _db;

    public ResourceRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Resource>> GetAllAsync()
    {
        return await _db.Resources.ToListAsync();
    }

    public async Task<Resource> GetByIdAsync(int id)
    {
        var result = await _db.Resources.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new ResourceNotFoundException("Resource not found");
    }

    public async Task<Resource> UpdateAsync(Resource t)
    {
        var dbResource = await _db.Resources.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbResource == null)
        {
            throw new ResourceNotFoundException("Resource not found");
        }

        dbResource.Name = t.Name;
        dbResource.Url = t.Url;
        dbResource.Type = t.Type;
            
        await _db.SaveChangesAsync();
        return dbResource;
    }

    public async Task<Resource> CreateAsync(Resource t)
    {
        var result = await _db.Resources.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Resource t)
    {
        var result = _db.Resources.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }
}