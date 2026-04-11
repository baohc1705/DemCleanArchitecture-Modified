using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.DeleteNews
{
    public class DeleteNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
