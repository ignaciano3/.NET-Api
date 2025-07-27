using PruebaApi.Models;

namespace PruebaApi.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByProductIdAsync(int productId);
    }
}
