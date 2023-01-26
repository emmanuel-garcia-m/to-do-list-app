using System.Collections.Generic;

namespace ToDoListApp.Domain.Models
{
    public class ToDoList : BaseDomainModel
    {
        public string description { get; set; }

        public ICollection<ToDoTask> TasksList { get; set; }
    }
}
