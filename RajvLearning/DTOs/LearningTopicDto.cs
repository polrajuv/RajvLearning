namespace RajvLearning.API.DTOs
{
    public class LearningTopicDto
    {
        public int TopicId { get; set; }


        public string Title { get; set; } = string.Empty;


        public string Category { get; set; } = string.Empty;


        // What is this topic?
        public string Definition { get; set; } = string.Empty;


        // Short overview
        public string Summary { get; set; } = string.Empty;


        // Main learning content
        public string Content { get; set; } = string.Empty;


        // Code examples / samples
        public string ExampleContent { get; set; } = string.Empty;


        // Notes / interview tips
        public string Notes { get; set; } = string.Empty;


        // External links
        public string References { get; set; } = string.Empty;


        // Search keywords
        public string Tags { get; set; } = string.Empty;


        public bool IsPublished { get; set; }


        public DateTime CreatedDate { get; set; }


        public DateTime? UpdatedDate { get; set; }
    }
}