using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.PublishNews
{
    public class PublishNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime? ScheduledAt { get; set; }
    }
}
