using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByProductIdAsync(int productId);
    }
}
