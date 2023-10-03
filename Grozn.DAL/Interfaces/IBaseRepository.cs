using Grozn.Domain.Entities;

namespace Grozn.DAL.Interfaces;

public interface IBaseRepository<T> where T: BaseEntity
{
    void CreateAsync(T entity);

    Task<T> GetAsync(int id);

    IEnumerable<User> SelectAsync();

    void DeleteAsync(int id);
}