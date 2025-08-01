﻿using PruebaApi.Models;

namespace PruebaApi.Interfaces
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<bool> ExistsAsync(int id);
        Task<List<Product>> GetProductsFromVendor(int vendorId);
    }
}
