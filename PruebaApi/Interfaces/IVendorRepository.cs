using PruebaApi.Models;

namespace PruebaApi.Interfaces
{
    public interface IVendorRepository: IRepository<Vendor>
    {
        Task<List<Product>> GetProductsFromVendor(int vendorId);
    }
}
