﻿namespace PruebaApi.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
