using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using DemoCleanArchitectureV2.Infrastructure.Data;

namespace DemoCleanArchitectureV2.Infrastructure.Repositories
{
    public class MenuNewsRepository : BaseRepository<NewsMenu, int>, INewsMenuRepository
    {
        public MenuNewsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
