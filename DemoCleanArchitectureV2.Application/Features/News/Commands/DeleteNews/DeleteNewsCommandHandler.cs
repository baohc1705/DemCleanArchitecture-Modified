using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.DeleteNews
{
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, int>
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        public DeleteNewsCommandHandler(INewsRepository newsRepository, INewsMenuRepository newsMenuRepository)
        {
            this.newsRepository = newsRepository;
            this.newsMenuRepository = newsMenuRepository;
        }
        public async Task<int> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {

            var news = await newsRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id && n.IsActive) 
                ?? throw new Exception("Not found");

            // Soft Delete News
            news.IsActive = false;
            news.DeletedAt = DateTime.UtcNow;
            news.UpdatedAt = DateTime.UtcNow;

            newsRepository.Update(news);
        
            var newsMenuList = await newsMenuRepository.GetListAsync(nm => nm.NewsId == request.Id);

            if (newsMenuList.Any())
            {
                
                foreach (var nm in newsMenuList)
                {
                    nm.IsActive = false;
                    nm.DeletedAt = DateTime.UtcNow;
                    nm.UpdatedAt = DateTime.UtcNow;
                }

                newsMenuRepository.UpdateRange(newsMenuList);
            }
            return news.Id;
        }
    }
}
