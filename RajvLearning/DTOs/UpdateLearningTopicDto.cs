namespace RajvLearning.API.DTOs
{
    public class UpdateLearningTopicDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Difficulty { get; set; } = "Beginner";

        public bool IsPublished { get; set; }
    }
}