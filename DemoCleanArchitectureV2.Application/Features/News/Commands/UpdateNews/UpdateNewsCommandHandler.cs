using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.UpdateNews
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, int>
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsMenuRepository newsMenuRepository;

        public UpdateNewsCommandHandler(INewsRepository newsRepository, INewsMenuRepository newsMenuRepository)
        {
            this.newsRepository = newsRepository;
            this.newsMenuRepository = newsMenuRepository;

        }
        public async Task<int> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await newsRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id && n.IsActive && n.DeletedAt == null)
                ?? throw new Exception("News không tồn tại hoặc đã bị xóa.");

            news.Title = request.Title;
            news.Slug = request.Slug;
            news.Summary = request.Summary;
            news.ThumbnailUrl = request.ThumbnailUrl;
            news.UpdatedAt = DateTime.UtcNow;

            newsRepository.Update(news);

            if (request.MenuIds != null)
            {
                var existingMenuNews = await newsMenuRepository.GetListAsync(nm => nm.NewsId == news.Id);


                var existingMenuIds = existingMenuNews.Select(nm => nm.MenuId).ToList();

                // Có trong DB nhưng không có trong request mới
                var toDelete = existingMenuNews.Where(nm => !request.MenuIds.Contains(nm.MenuId)).ToList();
                if (toDelete.Any())
                {
                    
                    //newsMenuRepository.RemoveRange(toDelete);

                    foreach(var item in toDelete) { item.IsActive = false; item.DeletedAt = DateTime.UtcNow; }
                    newsMenuRepository.UpdateRange(toDelete);
                }

                // Có trong request nhưng chưa có trong DB
                var toAddIds = request.MenuIds.Except(existingMenuIds).ToList();
                if (toAddIds.Any())
                {
                    var newMenuNews = toAddIds.Select(menuId => new NewsMenu
                    {
                        NewsId = news.Id,
                        MenuId = menuId,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    }).ToList();

                    await newsMenuRepository.AddRangeAsync(newMenuNews);
                }
            }
      
            return news.Id;
        }
    }
}
