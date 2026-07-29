using System.ComponentModel.DataAnnotations;

namespace RajvLearning.API.DTOs
{
    public class CreateLearningTopicDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 5,
            ErrorMessage = "Title must be between 5 and 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Definition { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        [MinLength(20, ErrorMessage = "Content must contain at least 20 characters.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100,
            ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Difficulty is required.")]
        //[RegularExpression("^(Beginner|Intermediate|Advanced)$",
        //    ErrorMessage = "Difficulty must be Beginner, Intermediate or Advanced.")]
        //public string Difficulty { get; set; } = "Beginner";

        public bool IsPublished { get; set; } = true;
        public string Notes {  get; set; } = string.Empty;
        public string References { get; set; } = string.Empty;
        public string ExampleContent {  get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

    }
}