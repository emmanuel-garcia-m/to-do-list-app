using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.TodoList.Command.CreateToDoList;
using ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername;

namespace ToDoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get To do list by userName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}", Name = "GetToDoListByUsername")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ToDoListVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ToDoListVm>>> GetToDoListByUsername(string username)
        {
            GetToDoListQuery query = new GetToDoListQuery(username);
            List<ToDoListVm> videos = await _mediator.Send(query);
            return Ok(videos);
        }

        /// <summary>
        ///  Create a new To do List
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ToDoListVm>> CreateTodoList([FromBody] CreateToDoListCommand command)
        {            
            command.CreatedBy = User.FindFirst("Uid")?.Value;
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(command));
        }
    }
}
