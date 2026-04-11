using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public List<int> News { get; set; }


    }
}
