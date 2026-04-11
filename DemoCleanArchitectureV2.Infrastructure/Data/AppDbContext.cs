using DemoCleanArchitectureV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoCleanArchitectureV2.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<News> News => Set<News>();
        public DbSet<NewsMenu> NewsMenus => Set<NewsMenu>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tự động load toàn bộ configuration trong assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
