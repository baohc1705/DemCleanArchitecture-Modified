namespace DemoCleanArchitectureV2.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public ICollection<NewsMenu> NewsMenu { get; set; } = new List<NewsMenu>();
    }
}
