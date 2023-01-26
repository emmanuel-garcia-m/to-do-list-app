using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoListApp.Application.Features.ToDoTask.Command.UpdateToDoTask
{
    public class UpdateToDoTaskCommand : IRequest
    {
        [Required]        
        public int Id { get; set; }

        [JsonIgnore]
        public string LastUpdatedBy { get; set; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 0)]
        public string description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public int ToDoListId { get; set; }
    }
}
