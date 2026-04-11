using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.UnpublishNews
{
    public class UnpublishNewsCommandHandler : IRequestHandler<UnpublishNewsCommand, int>
    {
        private readonly INewsRepository newsRepository;

        public UnpublishNewsCommandHandler(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task<int> Handle(UnpublishNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await newsRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id && n.IsActive)
                ?? throw new Exception("Not found");

            if (news.Status != NewsStatus.Published)
                throw new Exception("Bài viết phải đang ở trạng thái Published");

            news.Status = NewsStatus.Archived;
            news.UpdatedAt = DateTime.UtcNow;

            newsRepository.Update(news);

            return news.Id;
        }
    }
}
