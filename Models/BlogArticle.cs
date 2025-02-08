using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class BlogArticle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Perex { get; set; } = string.Empty;

        public string Keywords { get; set; } = "";

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? ImageSource { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
