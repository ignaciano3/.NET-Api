using Microsoft.AspNetCore.Http.HttpResults;
using PruebaApi.Dtos.Product;
using PruebaApi.Dtos.Vendor;
using PruebaApi.Interfaces;
using PruebaApi.Mappers;

namespace PruebaApi.Endpoints
{
    public static class VendorEndpoints
    {
        public static WebApplication MapVendors(this WebApplication app)
        {
            var group = app.MapGroup("/api/vendors")
                .WithTags("Vendors")
                .RequireAuthorization();

            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id:int}", Update);
            group.MapDelete("/{id:int}", Delete);

            group.MapGet("/{id:int}/products", GetAllProducts);


            return app;
        }

        public static async Task<List<VendorDto>> GetAll(IVendorRepository repository)
        {
            var vendors = await repository.GetAllAsync();
            var vendorsDto = vendors.Select(v => v.ToVendorDto()).ToList();
            return vendorsDto;
        }

        public static async Task<Results<Ok<VendorDto>, NotFound>> GetById(int id, IVendorRepository repository)
        {
            var vendor = await repository.GetByIdAsync(id);

            if (vendor is null)
            {
                return TypedResults.NotFound();
            }

            var vendorDto = vendor.ToVendorDto();
            return TypedResults.Ok(vendorDto);
        }

        public static async Task<Created<VendorDto>> Create(CreateVendorDto vendorDto, IVendorRepository repository)
        {
            var vendor = vendorDto.ToVendor();
            vendor = await repository.AddAsync(vendor);
            return TypedResults.Created($"/api/vendors/{vendor.Id}", vendor.ToVendorDto());
        }

        public static async Task<Results<NoContent, NotFound>> Update(int id, UpdateVendorDto updateVendor,
            IVendorRepository repository)
        {
            var vendor = updateVendor.ToVendor();
            vendor = await repository.UpdateAsync(id, vendor);

            if (vendor is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }

        public static async Task<Results<NoContent, NotFound>> Delete(int id, IVendorRepository repository)
        {
            var ok = await repository.DeleteAsync(id);
            return ok ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        private static async Task<List<ProductDto>> GetAllProducts(int id, IProductRepository productRepo)
        {
            var products = await productRepo.GetProductsFromVendor(id);
            return products.Select(p => p.ToProductDto()).ToList();
        }
    }
}
