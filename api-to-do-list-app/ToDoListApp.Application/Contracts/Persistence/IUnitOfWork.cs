using System;
using System.Threading.Tasks;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IToDoListRepository ToDoListRepository { get; }
        IToDoTaskRepository ToDoTaskRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        Task<int> Complete();
    }
}
