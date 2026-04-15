using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusUseProxies
{
    public class GetMenusUseProxiesQueryHandler : IRequestHandler<GetMenusUseProxiesQuery, MenuDto>
    {
        private readonly IMenuRepository menuRepository;
        public GetMenusUseProxiesQueryHandler(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }
        public async Task<MenuDto> Handle(GetMenusUseProxiesQuery request, CancellationToken cancellationToken)
        {
            // Tìm kiếm theo id để lấy menu 
            var menu = await menuRepository.GetByIdAsync(request.Id);

            var dto = new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name,
                News = menu.NewsMenu            // Gọi bên bảng news proxies sử dụng lazy load
                .Where(nm => nm.News != null)
                .Select(nm => new NewsDto       // Vấn đề N+1 Query (Quá nhiều query cho một lần)
                {
                    Id = nm.News.Id,
                    Title = nm.News.Title
                }).ToList()
            };

            return dto;
        }
    }
}
