using DemoCleanArchitectureV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCleanArchitectureV2.Infrastructure.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                   .HasColumnName("new_id");

            builder.Property(n => n.Title)
                   .HasColumnName("new_title")
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(n => n.Slug)
                   .HasColumnName("new_slug")
                   .HasMaxLength(500);

            builder.Property(n => n.Summary)
                   .HasColumnName("new_summary")
                   .HasMaxLength(1000);

            builder.Property(n => n.Content)
                   .HasColumnName("new_content")
                   .HasColumnType("nvarchar(max)");

            builder.Property(n => n.ThumbnailUrl)
                   .HasColumnName("new_thumbnail")
                   .HasMaxLength(500);

            builder.Property(n => n.Status)
                   .HasConversion(
                       v => v.ToString(),
                       v => (NewsStatus)Enum.Parse(typeof(NewsStatus), v)
                   )
                   .HasColumnName("new_status")
                   .HasMaxLength(50);

            builder.Property(n => n.PublishedAt)
                   .HasColumnName("new_published_at");

            builder.Property(n => n.ViewCount)
                   .HasColumnName("new_view_count")
                   .HasDefaultValue(0);

            builder.Property(n => n.IsActive)
                   .HasColumnName("new_is_active")
                   .HasDefaultValue(true);

            builder.Property(n => n.CreatedAt)
                   .HasColumnName("new_created_at");

            builder.Property(n => n.UpdatedAt)
                   .HasColumnName("new_updated_at");

            builder.Property(n => n.DeletedAt)
                   .HasColumnName("new_deleted_at");

            builder.Property(n => n.CreatedBy)
                   .HasColumnName("new_created_by")
                   .HasMaxLength(100);

            builder.Property(n => n.UpdatedBy)
                   .HasColumnName("new_updated_by")
                   .HasMaxLength(100);

            //builder.HasMany(n => n.NewsMenu)
            //       .WithOne(nm => nm.News)
            //       .HasForeignKey(nm => nm.NewsId)
            //       .HasPrincipalKey(n => n.Id)
            //       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
