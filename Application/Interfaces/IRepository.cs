namespace Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T item);
        Task<T?> UpdateAsync(int id, T updatedItem);
        Task<bool> DeleteAsync(int id);
    }
}
