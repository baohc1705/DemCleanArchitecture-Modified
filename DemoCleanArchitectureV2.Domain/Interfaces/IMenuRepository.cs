using DemoCleanArchitectureV2.Domain.Entities;

namespace DemoCleanArchitectureV2.Domain.Interfaces
{
    public interface IMenuRepository : IBaseRepository<Menu, int>
    {
        Task<IEnumerable<Menu>> GetMenusRawSQL();
    }
}
