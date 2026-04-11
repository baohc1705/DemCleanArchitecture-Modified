using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.CreateNews
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, int>
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsMenuRepository newsMenuRepository;
        public CreateNewsCommandHandler(INewsRepository newsRepository,INewsMenuRepository menuNewsRepository)
        {
            this.newsRepository = newsRepository;
            this.newsMenuRepository = menuNewsRepository;
            
        }
        public async Task<int> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var existsSlug = await newsRepository.ExistsAsync(n => n.Slug == request.Slug);

            if (existsSlug)
                throw new Exception("Slug đã tồn tại");

            var news = new Domain.Entities.News
            {
                Title = request.Title,
                Slug = request.Slug,
                Summary = request.Summary,
                Content = request.Content,
                ThumbnailUrl = request.ThumbnailUrl,
                Status = NewsStatus.Draft,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            await newsRepository.AddAsync(news);

            if (request.MenuIds != null && request.MenuIds.Any())
            {
                var menuNewsList = request.MenuIds.Select(menuId => new NewsMenu
                {
                    NewsId = news.Id,
                    MenuId = menuId,
                    IsActive = true,
                    CreatedAt= DateTime.Now,
                }).ToList();

                await newsMenuRepository.AddRangeAsync(menuNewsList);
            }

            return news.Id;
        }
    }
}
