using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Infrastructure.Context;

namespace ToDoListApp.Infrastructure.Repositories
{
    public class ToDoListRepository : RepositoryBase<Domain.Models.ToDoList>, IToDoListRepository
    {
        public ToDoListRepository(ToDoListdbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Domain.Models.ToDoList>> GetToDoListsByUserName(string username)
        {
            return await _context.ToDoLists!.Where(v => v.CreatedBy == username).ToListAsync();
        }
    }
}
