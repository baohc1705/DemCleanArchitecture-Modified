namespace DemoCleanArchitectureV2.Domain.Entities
{
    public enum NewsStatus
    {
        Draft = 0,
        Scheduled = 1,
        Published = 2,
        Archived = 3
    }
    public class News : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public NewsStatus Status { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public ICollection<NewsMenu> NewsMenu { get; set; } = new List<NewsMenu>();
    }
}
