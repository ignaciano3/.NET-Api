using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comment: Entity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [StringLength(255)]
        public required string Title { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
