namespace ToDoListApp.Application.Features.ToDoTask
{
    public class ToDoTaskVm
    {
        public int Id { get; set; }
        public string description { get; set; }

        public bool IsCompleted { get; set; }

        public int ToDoListId { get; set; }
    }
}
