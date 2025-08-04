using Application.Dtos.Comment;
using Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Mappers;

namespace PruebaApi.Endpoints
{
    public static class CommentEndpoints
    {
        public static WebApplication MapComments(this WebApplication app)
        {
            var group = app.MapGroup("/api/comments")
                .WithTags("Comments")
                .RequireAuthorization();

            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id:int}", Update);
            group.MapDelete("/{id:int}", Delete);

            return app;
        }

        private static async Task<Ok<List<CommentDto>>> GetAll(ICommentRepository repository)
        {
            var comments = await repository.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto()).ToList();

            return TypedResults.Ok(commentsDto);
        }

        private static async Task<Results<Ok<CommentDto>, NotFound>> GetById(int id, ICommentRepository repository)
        {
            var comment = await repository.GetByIdAsync(id);

            if (comment is null)
            {
                return TypedResults.NotFound();
            }

            var commentDto = comment.ToCommentDto();
            return TypedResults.Ok(commentDto);
        }

        private static async Task<Results<Created<CommentDto>, BadRequest<string>>> Create(CreateCommentDto commentDto, ICommentRepository repository, IProductRepository productRepo)
        {
            if (!await productRepo.ExistsAsync(commentDto.ProductId))
            {
                return TypedResults.BadRequest($"Product with ID {commentDto.ProductId} does not exist.");
            }

            var comment = commentDto.ToComment(commentDto.ProductId);
            comment = await repository.AddAsync(comment);

            return TypedResults.Created($"/api/comments/{comment.Id}", comment.ToCommentDto());
        }

        private static async Task<Results<NoContent, NotFound>> Update(int id, UpdateCommentDto updateComment, ICommentRepository repository)
        {
            var comment = updateComment.ToComment();
            comment = await repository.UpdateAsync(id, comment);

            if (comment is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }

        private static async Task<Results<NoContent, NotFound>> Delete(int id, ICommentRepository repository)
        {
            var ok = await repository.DeleteAsync(id);
            return ok ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
