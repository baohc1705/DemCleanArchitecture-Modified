using DemoCleanArchitectureV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCleanArchitectureV2.Infrastructure.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("menu_id");

            builder.Property(m => m.Name)
                .HasColumnName("menu_name");

            builder.Property(m => m.Slug)
                .HasColumnName("menu_slug");

            builder.Property(m => m.DisplayOrder)
                .HasColumnName("menu_display_order");

            builder.Property(m => m.IsActive)
                .HasColumnName("menu_is_active");

            builder.Property(m => m.CreatedAt)
               .HasColumnName("menu_created_at");

            builder.Property(m => m.UpdatedAt)
               .HasColumnName("menu_updated_at");

            builder.Property(m => m.DeletedAt)
               .HasColumnName("menu_deleted_at");

            builder.Property(m => m.CreatedBy)
               .HasColumnName("menu_created_by");

            builder.Property(m => m.UpdatedBy)
               .HasColumnName("menu_updated_by");


            //builder.HasMany(m => m.MenuNews)
            //    .WithOne(mn => mn.Menu)
            //    .HasForeignKey(mn => mn.MenuId)
            //    .HasPrincipalKey(m => m.Id)
            //    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
