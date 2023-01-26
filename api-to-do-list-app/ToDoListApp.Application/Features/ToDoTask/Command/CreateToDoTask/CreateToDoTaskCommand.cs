using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoListApp.Application.Features.ToDoTask.Command.CreateToDoTask
{
    public class CreateToDoTaskCommand : IRequest<ToDoTaskVm>
    { 
        [JsonIgnore]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 0)]
        public string description { get; set; }

        [Required]
        public int ToDoListId { get; set; }
    }
}
