using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }

        [StringLength(255, MinimumLength = 3, 
            ErrorMessage = "Value must be between 3 and 255 characters.")]
        public required string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
