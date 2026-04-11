namespace DemoCleanArchitectureV2.Domain.Entities
{
    public class NewsMenu : BaseEntity
    {
        public int NewsId { get; set; }
        public int MenuId { get; set; }
        public bool IsActive { get; set; }
        public  News News { get; set; }
        public  Menu Menu { get; set; }
    }
}
