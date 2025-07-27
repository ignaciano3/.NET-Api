using Microsoft.EntityFrameworkCore;
using PruebaApi.Data;
using PruebaApi.Interfaces;
using PruebaApi.Models;

namespace PruebaApi.Repository
{
    public class VendorRepository: BaseRepository<Vendor>, IVendorRepository
    {
        private readonly ApplicationDbContext _context;
        public VendorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsFromVendor(int vendorId)
        {
            var vendor = await _context.Vendors
                .Include(v => v.Products)
                .FirstOrDefaultAsync(v => v.Id == vendorId);

            return vendor?.Products ?? [];
        }
    }
}
