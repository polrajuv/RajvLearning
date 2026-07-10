namespace RajvLearning.API.DTOs
{
    public class LearningTopicDto
    {
        public int TopicId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string DifficultyLevel { get; set; } = string.Empty;

        public bool IsPublished { get; set; }

    }
}