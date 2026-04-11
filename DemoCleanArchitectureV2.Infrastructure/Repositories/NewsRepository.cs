using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using DemoCleanArchitectureV2.Infrastructure.Data;

namespace DemoCleanArchitectureV2.Infrastructure.Repositories
{
    public class NewsRepository : BaseRepository<News, int>, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
