using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
