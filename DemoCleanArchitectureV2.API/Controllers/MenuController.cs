using DemoCleanArchitectureV2.API.Common;
using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Application.Features.Menus.Commands.CreateMenu;
using DemoCleanArchitectureV2.Application.Features.Menus.Commands.DeleteMenu;
using DemoCleanArchitectureV2.Application.Features.Menus.Commands.UpdateMenu;
using DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenus;
using DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusDapper;
using DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusIQueryable;
using DemoCleanArchitectureV2.Application.Features.Menus.Queries.GetMenusSpec;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCleanArchitectureV2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ISender mediator;
        public MenuController (ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var data = await mediator.Send(new GetMenusQuery());
            return Ok(ApiResponse<IEnumerable<MenuDto>>.Ok(data, "Get all success"));
        }

        [HttpGet("GetMenusIQuery")]
        public async Task<IActionResult> GetMenusIQuery()
        {
            var data = await mediator.Send(new GetMenusIQueryableQuery());
            return Ok(ApiResponse<IEnumerable<MenuDto>>.Ok(data, "Get all success"));
        }

        [HttpGet("GetMenusSpec")]
        public async Task<IActionResult> GetMenusSpec()
        {
            var data = await mediator.Send(new GetMenusSpecQuery());
            return Ok(ApiResponse<IEnumerable<MenuDto>>.Ok(data, "Get all success"));
        }

        [HttpGet("GetMenusDapper")]
        public async Task<IActionResult> GetMenusDapper()
        {
            var data = await mediator.Send(new GetMenusDapperQuery());
            return Ok(ApiResponse<IEnumerable<MenuDto>>.Ok(data, "Get all success"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu(CreateMenuCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(ApiResponse<int>.Ok(data, "Create success"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var data = await mediator.Send(new DeleteMenuCommand() { Id = id});
            return Ok(ApiResponse<int>.Ok(data, "delete success"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, UpdateMenuCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiResponse<int>.Fail("Id not same"));
            var data = await mediator.Send(command);
            return Ok(ApiResponse<int>.Ok(data, "delete success"));
        }
    }
}
