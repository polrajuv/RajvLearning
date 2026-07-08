namespace RajvLearning.API.DTOs
{
    public class CreateLearningTopicDto
    {
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string DifficultyLevel { get; set; } = "Beginner";

        public bool IsPublished { get; set; } = true;
    }
}