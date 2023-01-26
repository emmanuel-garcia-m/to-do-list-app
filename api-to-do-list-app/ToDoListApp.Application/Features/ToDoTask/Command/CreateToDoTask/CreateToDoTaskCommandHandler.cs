using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using domain = ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Features.ToDoTask.Command.CreateToDoTask
{
    public class CreateToDoTaskCommandHandler : IRequestHandler<CreateToDoTaskCommand, ToDoTaskVm>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateToDoTaskCommandHandler> _logger;

        public CreateToDoTaskCommandHandler(IUnitOfWork unitOfWork, IToDoTaskRepository toDoTaskRepository, IMapper mapper, ILogger<CreateToDoTaskCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _toDoTaskRepository = toDoTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ToDoTaskVm> Handle(CreateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var requetDomainModel = _mapper.Map<domain.ToDoTask>(request);

            _toDoTaskRepository.AddEntity(requetDomainModel);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"No se pudo insertar el registro");
            }           

            return _mapper.Map<ToDoTaskVm>(requetDomainModel);
        }
    }
}