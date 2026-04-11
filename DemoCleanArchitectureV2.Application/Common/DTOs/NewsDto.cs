namespace DemoCleanArchitectureV2.Application.Common.DTOs
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string Status { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public List<MenuShortDto> Menus { get; set; }
    }
}
