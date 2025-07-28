using Microsoft.EntityFrameworkCore;
using PruebaApi.Data;
using PruebaApi.Interfaces;
using PruebaApi.Models;

namespace PruebaApi.Repositories
{
    public class CommentRepository: BaseRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsByProductIdAsync(int productId)
        {
            return await _context.Comments
                .Where(c => c.ProductId == productId)
                .ToListAsync();
        }

        public override async Task<Comment?> UpdateAsync(int id, Comment updatedComment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
                return null;

            existingComment.Content = updatedComment.Content;
            existingComment.Title = updatedComment.Title;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}
