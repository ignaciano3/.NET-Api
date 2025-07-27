using Microsoft.AspNetCore.Http.HttpResults;
using PruebaApi.Dtos.Comment;
using PruebaApi.Dtos.Product;
using PruebaApi.Interfaces;
using PruebaApi.Mappers;
using PruebaApi.Models;

namespace PruebaApi.Endpoints
{
    public static class ProductsEndpoints
    {
        public static WebApplication MapProducts(this WebApplication app)
        {
            var group = app.MapGroup("/api/products")
                .WithTags("Products")
                .RequireAuthorization();

            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id:int}", Update);
            group.MapDelete("/{id:int}", Delete);

            group.MapGet("/{id:int}/comments", GetAllComments);

            return app;
        }

        public static async Task<List<ProductDto>> GetAll(IProductRepository repository)
        {
            var products = await repository.GetAllAsync();
            var productsDto = products.Select(p => p.ToProductDto()).ToList();
            return productsDto;
        }

        public static async Task<Results<Ok<ProductDto>, NotFound>> GetById(int id, IProductRepository repository)
        {
            var product = await repository.GetByIdAsync(id);
            return product is not null ? TypedResults.Ok(product.ToProductDto()) : TypedResults.NotFound();
        }

        public static async Task<Created<ProductDto>> Create(CreateProductDto productDto,
            IProductRepository repository)
        {
            var product = productDto.ToProduct();
            product = await repository.AddAsync(product);
            return TypedResults.Created($"/api/products/{product.Id}", product.ToProductDto());
        }

        public static async Task<Results<NoContent, NotFound>> Update(int id, UpdateProductDto updatedProduct,
            IProductRepository repository)
        {
            var product = updatedProduct.ToProduct();
            product = await repository.UpdateAsync(id, product);
            return product is not null ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        public static async Task<Results<NoContent, NotFound>> Delete(int id, IProductRepository repository)
        {
            var ok = await repository.DeleteAsync(id);
            return ok ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        public static async Task<List<CommentDto>> GetAllComments(int id, ICommentRepository commentRepo)
        {
            var comments = await commentRepo.GetCommentsByProductIdAsync(id);
            return comments.Select(c => c.ToCommentDto()).ToList();
        }
    }
}