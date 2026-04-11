using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.PublishNews
{
    internal class PublishNewsCommandHandler : IRequestHandler<PublishNewsCommand, int>
    {
        private readonly INewsRepository newsRepository;

        public PublishNewsCommandHandler(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task<int> Handle(PublishNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await newsRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id && n.IsActive)
                ?? throw new Exception("Not found");

            if (news.Status == Domain.Entities.NewsStatus.Published
                || news.Status == Domain.Entities.NewsStatus.Archived)
                throw new Exception("Bài viết đang ở trạng thái Published hoặc Archived");

            if (request.ScheduledAt.HasValue)
            {
                news.Status = Domain.Entities.NewsStatus.Scheduled;
                news.PublishedAt = request.ScheduledAt.Value;
                news.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                news.Status = Domain.Entities.NewsStatus.Published;
                news.PublishedAt = DateTime.UtcNow;
                news.UpdatedAt = DateTime.UtcNow;
            }

            newsRepository.Update(news);

            return news.Id;
        }
    }
}
