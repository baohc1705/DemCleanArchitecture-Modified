using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, int>
    {
        private readonly IMenuRepository menuRepository;
        private readonly INewsMenuRepository newsMenuRepository;
       
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, INewsMenuRepository newsMenuRepository)
        {
            this.menuRepository = menuRepository;
            this.newsMenuRepository = newsMenuRepository;
        }
        public async Task<int> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetFirstOrDefaultAsync(m => m.Id == request.Id && m.IsActive && m.DeletedAt == null)
                ?? throw new ArgumentException("Not found");
            
            menu.IsActive = false;
            menu.DeletedAt = DateTime.UtcNow;

            menuRepository.Update(menu);

            var newsMenuList = await newsMenuRepository.GetListAsync(nm => nm.MenuId == request.Id && nm.IsActive);
            if (newsMenuList.Any())
            {
                foreach (var item in newsMenuList)
                {
                    item.IsActive = false;
                    item.DeletedAt = DateTime.UtcNow;
                    item.UpdatedAt = DateTime.UtcNow;
                }

                newsMenuRepository.UpdateRange(newsMenuList);
            }
            return menu.Id;
        }
    }
}
