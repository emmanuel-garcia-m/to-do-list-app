namespace ToDoListApp.Domain.Models
{
    public class ToDoTask : BaseDomainModel
    { 
        public string description { get; set; }

        public bool IsCompleted { get; set; }

        public int ToDoListId { get; set; }

        public ToDoList ToDoList { get; set; }
    }
}
