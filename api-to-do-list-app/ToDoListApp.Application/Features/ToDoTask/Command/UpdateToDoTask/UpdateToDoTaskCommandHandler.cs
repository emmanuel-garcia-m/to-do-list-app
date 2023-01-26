using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Application.PersonalizedExceptions;
using domain = ToDoListApp.Domain.Models;


namespace ToDoListApp.Application.Features.ToDoTask.Command.UpdateToDoTask
{
    public class UpdateToDoTaskCommandHandler : IRequestHandler<UpdateToDoTaskCommand>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateToDoTaskCommandHandler> _logger;

        public UpdateToDoTaskCommandHandler(IUnitOfWork unitOfWork, IToDoTaskRepository toDoTaskRepository, IMapper mapper, ILogger<UpdateToDoTaskCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _toDoTaskRepository = toDoTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var requetDomainModel = _mapper.Map<domain.ToDoTask>(request);            

            var taskToUpdate = await _toDoTaskRepository.GetByIdAsync(request.Id);

            if (taskToUpdate == null)
            {
                _logger.LogError($"No se encontro la tarea de id {request.Id}");
                throw new NotFoundException(nameof(domain.ToDoTask), request.Id);
            }

            _mapper.Map(request, taskToUpdate, typeof(UpdateToDoTaskCommand), typeof(domain.ToDoTask));

            await _toDoTaskRepository.UpdateAsync(taskToUpdate);
            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer {request.Id}");

            return Unit.Value;
        }

    }
}
