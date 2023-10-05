namespace Repository.Abstractions;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<T> UpdateAsync(T t);

    Task CreateAsync(T t);

    Task DeleteAsync(T t);
}

    