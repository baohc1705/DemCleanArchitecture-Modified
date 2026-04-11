using AutoMapper;
using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>
    {
        private readonly IMenuRepository menuRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        private readonly IMapper mapper;

        public CreateMenuCommandHandler(IMenuRepository menuRepository, INewsMenuRepository newsMenuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.newsMenuRepository = newsMenuRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var existsSlug = await menuRepository.ExistsAsync(n => n.Slug == request.Slug);

            if (existsSlug)
                throw new Exception("Slug đã tồn tại");

            var menu = mapper.Map<Menu>(request);
            menu.IsActive = true;
            await menuRepository.AddAsync(menu);

            if (request.News.Any())
            {
                var newsMenuList = request.News.Select(n => new NewsMenu
                {
                    NewsId = n,
                    MenuId = menu.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                });

                await newsMenuRepository.AddRangeAsync(newsMenuList);
            }

            return menu.Id;
        }
    }
}
