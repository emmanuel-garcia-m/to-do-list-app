using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Features.TodoList.Command.CreateToDoList
{
    public class CreateToDoListCommandHandler : IRequestHandler<CreateToDoListCommand, ToDoListVm>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateToDoListCommandHandler> _logger;

        public CreateToDoListCommandHandler(IUnitOfWork unitOfWork, IToDoListRepository toDoListRepository, IMapper mapper,  ILogger<CreateToDoListCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ToDoListVm> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
        {
            ToDoList requetDomainModel = _mapper.Map<ToDoList>(request);
            
            _toDoListRepository.AddEntity(requetDomainModel);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"No se pudo insertar el registro");
            }

            _logger.LogInformation($"la lista de tareas {requetDomainModel.description} fue creada existosamente");

            return _mapper.Map<ToDoListVm>(requetDomainModel) ;
        }
    }
}
