using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Application.PersonalizedExceptions;
using domain = ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Features.ToDoTask.Command.DeleteTodoTask
{
    public class DeleteToDoTaskCommandHandler : IRequestHandler<DeleteToDoTaskCommand>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteToDoTaskCommandHandler> _logger;

        public DeleteToDoTaskCommandHandler(IUnitOfWork unitOfWork, IToDoTaskRepository toDoTaskRepository, IMapper mapper, ILogger<DeleteToDoTaskCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _toDoTaskRepository = toDoTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToDelete = await _toDoTaskRepository.GetByIdAsync(request.Id);

            if (taskToDelete == null)
            {
                _logger.LogError($"No se encontro la tarea de id {request.Id}");
                throw new NotFoundException(nameof(domain.ToDoTask), request.Id);
            }

            await _toDoTaskRepository.DeleteAsync(taskToDelete);
            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}