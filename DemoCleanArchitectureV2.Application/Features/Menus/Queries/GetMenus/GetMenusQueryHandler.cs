using AutoMapper;
using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;
using System.Diagnostics;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenus
{
    public class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        private readonly INewsRepository newsRepository;
        private readonly IMapper mapper;
        public GetMenusQueryHandler(IMenuRepository menuRepository,
                                    INewsMenuRepository newsMenuRepository,
                                    INewsRepository newsRepository,
                                    IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.newsMenuRepository = newsMenuRepository;
            this.newsRepository = newsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            // lấy tất cả menus
            var menus = await menuRepository.GetAllAsync();

            // lấy toàn bộ newsMenu theo danh sách menuId 
            var menuIds = menus.Select(m => m.Id);
            var newsMenus = await newsMenuRepository.GetListAsync(nm => menuIds.Contains(nm.MenuId));

            // lấy toàn bộ news theo danh sách newsId 
            var newsIds = newsMenus.Select(nm => nm.NewsId).Distinct();
            var newsList = await newsRepository.GetListAsync(n => newsIds.Contains(n.Id));

            // Lookup dict để join O(1)
            var newsById = newsList.ToDictionary(n => n.Id);
            var newsMenuDict = newsMenus
                .GroupBy(nm => nm.MenuId)
                .ToDictionary(g => g.Key, g => g.ToList());

            
            var result = menus.Select(menu =>
            {
                var dto = mapper.Map<MenuDto>(menu);

                dto.News = newsMenuDict.TryGetValue(menu.Id, out var nmList)
                    ? nmList
                        .Where(nm => newsById.ContainsKey(nm.NewsId))
                        .Select(nm => mapper.Map<NewsDto>(newsById[nm.NewsId]))
                        .ToList()
                    : new List<NewsDto>();

                return dto;
            });

            
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return result;
        }
    }
}
