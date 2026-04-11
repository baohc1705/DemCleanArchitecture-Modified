using DemoCleanArchitectureV2.API.Common;
using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Application.Features.News.Commands.CreateNews;
using DemoCleanArchitectureV2.Application.Features.News.Commands.DeleteNews;
using DemoCleanArchitectureV2.Application.Features.News.Commands.UpdateNews;
using DemoCleanArchitectureV2.Application.Features.News.Queries.GetNewsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCleanArchitectureV2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ISender mediator;

        public NewsController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<NewsDto>> GetNewsById(int id)
        {
            var data = await mediator.Send(new GetNewsByIdQuery() { Id = id});
            return ApiResponse<NewsDto>.Ok(data, "Get thanh cong", StatusCodes.Status200OK);
        }

        [HttpPost]
        public async Task<ApiResponse<int>> Create(CreateNewsCommand command)
        {
            var data = await mediator.Send(command);
            return ApiResponse<int>.Ok(data, "Da them thanh cong", StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<int>> Delete(int id)
        {
            var data = await mediator.Send(new DeleteNewsCommand() { Id = id });
            return ApiResponse<int>.Ok(data, $"Đã xóa thành công news với id = {id}", StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<int>> Update(int id, UpdateNewsCommand command)
        {
            if (id != command.Id)
                return ApiResponse<int>.Fail("Id không giống", StatusCodes.Status400BadRequest);
            var data = await mediator.Send(command);
            return ApiResponse<int>.Ok(data, $"Đã update thành công news với id = {id}", StatusCodes.Status200OK);

        }
    }
}
