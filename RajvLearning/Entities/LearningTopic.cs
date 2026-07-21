using System.ComponentModel.DataAnnotations;

namespace RajvLearning.API.Models
{
    public class LearningTopic
    {
        public int Id { get; set; }


        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        [StringLength(100)]
        public string? Category { get; set; }


        // Short definition of the topic
        public string? Definition { get; set; }


        // Brief overview
        public string? Summary { get; set; }


        // Main learning content (TinyMCE HTML)
        public string? Content { get; set; }


        // Code samples, SQL, JSON, PowerShell, etc.
        public string? ExampleContent { get; set; }


        // Best practices, tips, interview notes
        public string? Notes { get; set; }


        // Documentation links, GitHub, articles
        public string? References { get; set; }


        // Search keywords
        [StringLength(500)]
        public string? Tags { get; set; }


        // Beginner / Intermediate / Advanced
        [StringLength(50)]
        public string? Difficulty { get; set; }


        public bool IsPublished { get; set; }


        public DateTime CreatedDate { get; set; }


        public DateTime? UpdatedDate { get; set; }
    }
}