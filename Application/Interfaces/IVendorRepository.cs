using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVendorRepository: IRepository<Vendor>
    {
        Task<Vendor?> GetByIdWithProducts(int id);
    }
}
