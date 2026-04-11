using DemoCleanArchitectureV2.Application.Common.DTOs;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusDapper
{
    public class GetMenusDapperQuery : IRequest<IEnumerable<MenuDto>>
    {
    }
}
