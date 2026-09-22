namespace RajvLearning.API.DTOs
{
    public class UpdateLearningTopicDto
    {
        public string Title { get; set; } = string.Empty;


        public string Category { get; set; } = string.Empty;


        // Short definition of the topic
        public string Definition { get; set; } = string.Empty;


        // Brief overview
        public string Summary { get; set; } = string.Empty;


        // Main learning content
        public string Content { get; set; } = string.Empty;


        // Code samples, JSON, SQL, PowerShell, React examples
        public string? ExampleContent { get; set; }


        // Tips, best practices, interview notes
        public string? Notes { get; set; }


        // External documentation links
        public string? References { get; set; }


        // Search keywords
        public string? Tags { get; set; }


        // Beginner / Intermediate / Advanced
       // public string Difficulty { get; set; } = "Beginner";


        public bool IsPublished { get; set; }
    }
}