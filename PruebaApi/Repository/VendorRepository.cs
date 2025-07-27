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

    }
}
