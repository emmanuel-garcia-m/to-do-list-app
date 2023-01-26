using MediatR;

namespace ToDoListApp.Application.Features.ToDoTask.Command.DeleteTodoTask
{
    public class DeleteToDoTaskCommand : IRequest
    {
        public int Id { get; set; }
    }
}
