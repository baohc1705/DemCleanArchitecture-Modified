using DemoCleanArchitectureV2.Application.Common.DTOs;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusIQueryable
{
    public class GetMenusIQueryableQuery : IRequest<IEnumerable<MenuDto>>
    {
    }
}
