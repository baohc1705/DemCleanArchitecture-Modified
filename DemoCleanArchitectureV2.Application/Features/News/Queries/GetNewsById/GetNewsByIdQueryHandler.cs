using AutoMapper;
using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Queries.GetNewsById
{
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, NewsDto>
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        public GetNewsByIdQueryHandler(INewsRepository newsRepository,
                                       INewsMenuRepository newsMenuRepository,
                                       IMenuRepository menuRepository,
                                       IMapper mapper)
        {
            this.newsRepository = newsRepository;
            this.newsMenuRepository = newsMenuRepository;
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }
        public async Task<NewsDto> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var news = await newsRepository.GetByIdAsync(request.Id)
                ?? throw new Exception("Not found");

            var res = mapper.Map<NewsDto>(news);

            var newsMenuList = await newsMenuRepository
                .GetListAsync(nm => nm.NewsId == request.Id
                    && nm.IsActive
                    && nm.DeletedAt == null
                );

            if (newsMenuList.Any())
            {
                var menuIds = newsMenuList.Select(nm => nm.MenuId).ToList();

                var menus = await menuRepository.GetListAsync(m => menuIds.Contains(m.Id));

                res.Menus = menus.Select(m => new MenuShortDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                }).ToList();
            }

            return res;
        }
    }
}
