namespace DemoCleanArchitectureV2.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<NewsMenu> NewsMenu { get; set; } = new List<NewsMenu>(); // sử dụng vitual không đúng tinh thần clean
    }
}
