using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        private readonly ApplicationDbContext _context;
        public VendorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vendor?> GetByIdWithProducts(int id)
        {
            return await _context.Vendors
                .Include(v => v.Products)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
