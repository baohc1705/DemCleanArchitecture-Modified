using AutoMapper;
using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, int>
    {
        private readonly IMenuRepository menuRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        private readonly IMapper mapper;
        public UpdateMenuCommandHandler(IMenuRepository menuRepository, INewsMenuRepository newsMenuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.newsMenuRepository = newsMenuRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetFirstOrDefaultAsync(m => m.Id == request.Id && m.IsActive && m.DeletedAt == null)
                 ?? throw new ArgumentException("Not found");

            var res = mapper.Map<Menu>(request);

            menuRepository.Update(res);

            if (request.News.Any())
            {
                var newsMenuList = await newsMenuRepository.GetListAsync(nm => nm.MenuId == request.Id && nm.IsActive && nm.DeletedAt == null);

                var menuIdDbs = newsMenuList.Select(m => m.MenuId).ToList();
                
                // DB co ma request khong
                var toDelete = newsMenuList.Where(nm => !request.News.Contains(nm.MenuId)).ToList();
                if (toDelete.Any())
                {
                    foreach (var item in newsMenuList)
                    {
                        item.IsActive = false;
                        item.DeletedAt = DateTime.Now;
                    }

                    newsMenuRepository.UpdateRange(newsMenuList);
                }

                // DB khong ma request co
                var toAddId = request.News.Except(menuIdDbs).ToList();
                if (toAddId.Any())
                {
                    var newsMenu = toAddId.Select(nm => new NewsMenu ()
                    {
                        MenuId = request.Id,
                        NewsId = nm,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    }).ToList();

                    await newsMenuRepository.AddRangeAsync(newsMenu);
                }
            }

            return menu.Id;
        }
    }
}
