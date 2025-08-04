using Application.Dtos.Comment;
using Domain.Entities;

namespace Application.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                ProductId = comment.ProductId
            };
        }

        public static Comment ToComment(this CreateCommentDto dto, int productId)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                ProductId = productId
            };
        }

        public static Comment ToComment(this UpdateCommentDto dto)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content
            };
        }
    }
}
