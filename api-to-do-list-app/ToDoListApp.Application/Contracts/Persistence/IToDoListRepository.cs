using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Contracts.Persistence
{
    public interface IToDoListRepository : IAsyncRepository <ToDoList>
    {
        Task<IEnumerable<ToDoList>> GetToDoListsByUserName(string username);
    }
}
