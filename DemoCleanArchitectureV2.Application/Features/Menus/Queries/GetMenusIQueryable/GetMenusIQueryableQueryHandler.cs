using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusIQueryable
{
    public class GetMenusIQueryableQueryHandler : IRequestHandler<GetMenusIQueryableQuery, IEnumerable<MenuDto>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        private readonly INewsRepository newsRepository;

        public GetMenusIQueryableQueryHandler(IMenuRepository menuRepository,
                                    INewsMenuRepository newsMenuRepository,
                                    INewsRepository newsRepository)
        {
            this.menuRepository = menuRepository;
            this.newsMenuRepository = newsMenuRepository;
            this.newsRepository = newsRepository;
        }
        public async Task<IEnumerable<MenuDto>> Handle(GetMenusIQueryableQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var menuQuery = await menuRepository.GetAllQueryAsync();
            var newsMenuQuery = await newsMenuRepository.GetAllQueryAsync();
            var newsQuery = await newsRepository.GetAllQueryAsync();

            var query = from menu in menuQuery
                      select new MenuDto
                      {
                          Id = menu.Id,
                          Name = menu.Name,
                          News = (from nm in newsMenuQuery
                                  join n in newsQuery on nm.NewsId equals n.Id
                                  where nm.MenuId == menu.Id
                                  select new NewsDto
                                  {
                                      Id = n.Id,
                                      Title = n.Title,
                                  }).ToList()
                      };
            var res = await query.ToListAsync(cancellationToken);
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return res;
        }
    }
}
