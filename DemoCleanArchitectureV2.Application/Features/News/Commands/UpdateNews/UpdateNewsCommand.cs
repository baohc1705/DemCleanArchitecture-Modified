using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.UpdateNews
{
    public class UpdateNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public List<int> MenuIds { get; set; } = new List<int>();
    }
}
