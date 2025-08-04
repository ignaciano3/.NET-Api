namespace Application.Dtos.Vendor
{
    public class CreateVendorDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Phone { get; set; }
    }
}
