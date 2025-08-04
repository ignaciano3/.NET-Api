using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Vendor: Entity
    {
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        public int Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; } = string.Empty;
        
        public List<Product> Products { get; set; } = new();
    }
}
