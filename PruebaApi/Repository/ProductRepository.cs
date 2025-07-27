using Microsoft.EntityFrameworkCore;
using PruebaApi.Data;
using PruebaApi.Interfaces;
using PruebaApi.Models;

namespace PruebaApi.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return null;
            }
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
        public async Task<List<Product>> GetProductsFromVendor(int vendorId)
        {
            return await _context.Products
                .Where(p => p.Vendors.Any(v => v.Id == vendorId))
                .ToListAsync();
        }
    }
}
