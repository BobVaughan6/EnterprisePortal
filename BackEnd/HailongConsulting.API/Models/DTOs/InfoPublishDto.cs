namespace HailongConsulting.API.Models.DTOs
{
    public class InfoPublishDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? PublishTime { get; set; }
        public int ViewCount { get; set; }
        public List<string>? Attachments { get; set; }
        public bool IsTop { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateInfoPublishDto
    {
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? PublishTime { get; set; }
        public bool IsTop { get; set; } = false;
    }

    public class UpdateInfoPublishDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? PublishTime { get; set; }
        public bool IsTop { get; set; }
    }
}