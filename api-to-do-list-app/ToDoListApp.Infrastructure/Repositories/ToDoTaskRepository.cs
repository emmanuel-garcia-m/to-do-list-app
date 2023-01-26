using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Domain.Models;
using ToDoListApp.Infrastructure.Context;

namespace ToDoListApp.Infrastructure.Repositories
{
    public class ToDoTaskRepository : RepositoryBase<ToDoTask>, IToDoTaskRepository
    {
        public ToDoTaskRepository(ToDoListdbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ToDoTask>> GetToDoTaskListsById(int toDoListId)
        {
            return await _context.ToDoTasks!.Where(t => t.ToDoListId == toDoListId).ToListAsync();
        }
    }
}
