using System.ComponentModel.DataAnnotations;

namespace RajvLearning.API.Entities;

public class LearningTopic
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Difficulty { get; set; } = "Beginner";

    public bool IsPublished { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}