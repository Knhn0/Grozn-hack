using Domain.Entities;

namespace Repository;

public interface IResourceRepository
{
    Task<List<Resource>> GetAllAsync();
    Task<Resource> GetByIdAsync(int id);
    Task<Resource> UpdateAsync(Resource t);
    Task<Resource> CreateAsync(Resource t);
    Task<bool> DeleteAsync(Resource t);
}