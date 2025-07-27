using System.ComponentModel.DataAnnotations;

namespace PruebaApi.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        public int Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; } = string.Empty;
        
        public List<Product> Products { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
