using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername;

namespace ToDoListApp.Application.Features.TodoList.Command.CreateToDoList
{
    public class CreateToDoListCommand : IRequest<ToDoListVm>
    {
        [JsonIgnore]
        public string CreatedBy { get; set; }        

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 0)]
        public string description { get; set; }
    }
}
