using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product: Entity
    {
        [StringLength(255)]
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public List<Vendor> Vendors { get; set; } = new();
    }
}