using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Contracts.Persistence
{
    public interface IToDoTaskRepository : IAsyncRepository<ToDoTask>
    {
        Task<IEnumerable<ToDoTask>> GetToDoTaskListsById(int toDoListId);
    }
}
