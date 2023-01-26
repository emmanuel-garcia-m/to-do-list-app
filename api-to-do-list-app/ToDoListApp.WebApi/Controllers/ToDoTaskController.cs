using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.ToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Command.CreateToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Command.DeleteTodoTask;
using ToDoListApp.Application.Features.ToDoTask.Command.UpdateToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Queries.GetTodoTaskByListId;

namespace ToDoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ToDoTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get To do list by userName
        /// </summary>
        /// <param name="toDoListId"></param>
        /// <returns></returns>
        [HttpGet("{toDoListId}")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ToDoTaskVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ToDoTaskVm>>> GetTodoTaskByListId(int toDoListId)
        {
            var query = new GetTodoTaskByListIdQuery(toDoListId);
            List<ToDoTaskVm> videos = await _mediator.Send(query);
            return Ok(videos);
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ToDoTaskVm), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ToDoTaskVm>> Create([FromBody] CreateToDoTaskCommand command)
        {
            command.CreatedBy = User.FindFirst("Uid")?.Value;
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Update a task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateToDoTaskCommand command)
        {
            command.LastUpdatedBy = User.FindFirst("Uid")?.Value;
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteToDoTaskCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
