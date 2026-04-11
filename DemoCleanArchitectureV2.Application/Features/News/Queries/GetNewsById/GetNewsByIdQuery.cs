using DemoCleanArchitectureV2.Application.Common.DTOs;
using MediatR;

namespace DemoCleanArchitectureV2.Application.Features.News.Queries.GetNewsById
{
    public class GetNewsByIdQuery : IRequest<NewsDto>
    {
        public int Id { get; set; }
    }
}
