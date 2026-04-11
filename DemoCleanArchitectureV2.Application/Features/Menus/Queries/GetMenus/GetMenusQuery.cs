using DemoCleanArchitectureV2.Application.Common.DTOs;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenus
{
    public class GetMenusQuery : IRequest<IEnumerable<MenuDto>>
    {
    }
}
