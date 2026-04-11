using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public List<int> News { get; set; }

    }
}
