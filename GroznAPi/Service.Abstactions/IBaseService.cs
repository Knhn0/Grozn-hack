namespace Service.Abstactions;

public interface IBaseService<T> where T: class
{
    Task CreateAsync(T t);
    
    Task<T> UpdateAsync(T t);
    
    Task<T?> GetByIdAsync(int id);
    
    Task DeleteAsync(T t);
    
    Task<List<T>> GetAllAsync();
}