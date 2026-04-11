using AutoMapper;
using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;
using System.Diagnostics;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusDapper
{
    internal class GetMenusDapperQueryHandler : IRequestHandler<GetMenusDapperQuery, IEnumerable<MenuDto>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        public GetMenusDapperQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetMenusDapperQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var data = await menuRepository.GetMenusRawSQL();

            var res =  mapper.Map<IEnumerable<MenuDto>>(data);
            sw.Stop();
            Console.WriteLine($"Time {sw.ElapsedMilliseconds} ms");
            return res;
        }
    }
}
