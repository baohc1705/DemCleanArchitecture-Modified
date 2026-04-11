namespace DemoCleanArchitectureV2.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public int? CreatedBy { get; set; } = 1;
        public int? UpdatedBy { get; set; } = 1;
    }
}
