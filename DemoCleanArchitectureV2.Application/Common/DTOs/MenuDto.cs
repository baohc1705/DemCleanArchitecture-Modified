namespace DemoCleanArchitectureV2.Application.Common.DTOs
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public List<NewsDto> News {get; set;}
    }

    public class MenuShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}
