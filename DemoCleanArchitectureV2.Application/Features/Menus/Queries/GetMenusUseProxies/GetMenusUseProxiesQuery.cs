using DemoCleanArchitectureV2.Application.Common.DTOs;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusUseProxies
{
    public class GetMenusUseProxiesQuery : IRequest<MenuDto>
    {
        public int Id { get; set; }
    }
}
