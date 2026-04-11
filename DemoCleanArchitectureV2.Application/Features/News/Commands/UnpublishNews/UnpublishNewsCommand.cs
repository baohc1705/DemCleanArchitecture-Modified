using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.UnpublishNews
{
    public class UnpublishNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
