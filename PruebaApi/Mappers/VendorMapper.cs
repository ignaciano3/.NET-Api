using PruebaApi.Dtos.Vendor;
using PruebaApi.Models;

namespace PruebaApi.Mappers
{
    public static class VendorMapper
    {
        public static VendorDto ToVendorDto(this Vendor vendor)
        {
            return new VendorDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email,
                Phone = vendor.Phone
            };
        }
        public static Vendor ToVendor(this VendorDto vendorDto)
        {
            return new Vendor
            {
                Id = vendorDto.Id,
                Name = vendorDto.Name,
                Email = vendorDto.Email,
                Phone = vendorDto.Phone
            };
        }

        public static Vendor ToVendor(this CreateVendorDto vendorDto)
        {
            return new Vendor
            {
                Name = vendorDto.Name,
                Email = vendorDto.Email,
                Phone = vendorDto.Phone
            };
        }

        public static Vendor ToVendor(this UpdateVendorDto dto)
        {
            return new Vendor
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };
        }
    }
}
