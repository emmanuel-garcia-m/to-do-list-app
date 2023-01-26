using System;
using System.Collections;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Domain.Models;
using ToDoListApp.Infrastructure.Context;

namespace ToDoListApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ToDoListdbContext _context;

        private IToDoListRepository _toDoListRepository;
        private IToDoTaskRepository _toDoTaskRepository;

        public IToDoListRepository ToDoListRepository => _toDoListRepository ??= new ToDoListRepository(_context);

        public IToDoTaskRepository ToDoTaskRepository => _toDoTaskRepository ??= new ToDoTaskRepository(_context);

        public UnitOfWork(ToDoListdbContext context)
        {
            _context = context;
        }

        public ToDoListdbContext ToDoListdbContext => _context;

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Err");
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
