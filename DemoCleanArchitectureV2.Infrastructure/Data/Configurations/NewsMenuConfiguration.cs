using DemoCleanArchitectureV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCleanArchitectureV2.Infrastructure.Data.Configurations
{
    public class NewsMenuConfiguration : IEntityTypeConfiguration<NewsMenu>
    {
        public void Configure(EntityTypeBuilder<NewsMenu> builder)
        {
            builder.ToTable("NewsMenus");

           
            builder.HasKey(nm => nm.Id);
            builder.Property(nm => nm.Id)
                   .HasColumnName("nm_id");

            builder.Property(nm => nm.NewsId)
                   .HasColumnName("nm_news_id")
                   .IsRequired();

            builder.Property(nm => nm.MenuId)
                   .HasColumnName("nm_menu_id")
                   .IsRequired();

            builder.Property(nm => nm.IsActive)
                .HasColumnName("nm_is_active");

            builder.Property(nm => nm.CreatedAt)
               .HasColumnName("nm_created_at");

            builder.Property(nm => nm.UpdatedAt)
               .HasColumnName("nm_updated_at");

            builder.Property(nm => nm.DeletedAt)
               .HasColumnName("nm_deleted_at");

            builder.Property(nm => nm.CreatedBy)
               .HasColumnName("nm_created_by");

            builder.Property(nm => nm.UpdatedBy)
               .HasColumnName("nm_updated_by");


            //builder.HasOne(nm => nm.News)
            //       .WithMany(n => n.NewsMenu)
            //       .HasForeignKey(nm => nm.NewsId)
            //       .HasPrincipalKey(n => n.Id)
            //       .OnDelete(DeleteBehavior.NoAction);


            //builder.HasOne(nm => nm.Menu)
            //       .WithMany(m => m.MenuNews)
            //       .HasForeignKey(nm => nm.MenuId)
            //       .HasPrincipalKey(m => m.Id)
            //       .OnDelete(DeleteBehavior.NoAction);
                         

            // Index 
            builder.HasIndex(nm => nm.NewsId)
                   .HasDatabaseName("IX_NewsMenus_NewsId");

            builder.HasIndex(nm => nm.MenuId)
                   .HasDatabaseName("IX_NewsMenus_MenuId");

            builder.HasIndex(nm => new { nm.NewsId, nm.MenuId })
                   .HasDatabaseName("IX_NewsMenus_NewsId_MenuId")
                   .IsUnique();
        }
    }
}
