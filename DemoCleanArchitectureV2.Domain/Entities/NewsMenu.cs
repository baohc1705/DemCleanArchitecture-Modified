namespace DemoCleanArchitectureV2.Domain.Entities
{
    public class NewsMenu : BaseEntity
    {
        public int NewsId { get; set; }
        public int MenuId { get; set; }
        public bool IsActive { get; set; }
        public virtual  News News { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
