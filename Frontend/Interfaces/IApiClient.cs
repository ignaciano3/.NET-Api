﻿using PruebaApi.Dtos.Product;
using PruebaApi.Models;

namespace Frontend.Interfaces
{
    public interface IApiClient
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> AddProductAsync(UpdateProductDto product);
        Task EditProductAsync(UpdateProductDto product, int id);
        Task<UpdateProductDto> GetProductByIdAsync(int id);
        Task DeleteProductAsync(int id);
    }
}
