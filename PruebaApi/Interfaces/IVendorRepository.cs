using PruebaApi.Models;

namespace PruebaApi.Interfaces
{
    public interface IVendorRepository: IRepository<Vendor>
    {
        Task<Vendor?> GetByIdWithProducts(int id);
    }
}
