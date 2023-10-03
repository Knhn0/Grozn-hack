using System.Linq.Expressions;

namespace Grozn.Services.Interfaces;

public interface IBaseCrudService<T> where T : class
{
    IEnumerable<T> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> UpdateAsync(T t);
    void CreateAsync(T t);
    
    void RemoveAsync(int t);
}