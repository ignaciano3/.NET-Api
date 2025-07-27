namespace PruebaApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
