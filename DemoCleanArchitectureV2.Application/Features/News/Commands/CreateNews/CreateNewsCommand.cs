using DemoCleanArchitectureV2.Application.Common.DTOs;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.CreateNews
{
    public class CreateNewsCommand : IRequest<int>
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public List<int> MenuIds { get; set; } = new List<int>(); // Nhận 1 hoặc nhiều Menu
    }
}
