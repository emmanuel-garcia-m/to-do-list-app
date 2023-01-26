using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Application.Features.ToDoTask.Queries.GetTodoTaskByListId
{
    public class GetTodoTaskByListIdQuery : IRequest<List<ToDoTaskVm>>
    {
        [Required]
        public int ToDoListId { get; set; }

        public GetTodoTaskByListIdQuery(int toDoListId)
        {
            ToDoListId = toDoListId;
        }
    }
    
}
