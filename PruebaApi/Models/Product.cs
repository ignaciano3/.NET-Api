using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public List<Vendor> Vendors { get; set; } = new();
    }
}