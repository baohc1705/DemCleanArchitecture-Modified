using DemoCleanArchitectureV2.Domain.Entities;

namespace DemoCleanArchitectureV2.Domain.Interfaces
{
    public interface INewsRepository : IBaseRepository<News, int>
    {
    }
}
