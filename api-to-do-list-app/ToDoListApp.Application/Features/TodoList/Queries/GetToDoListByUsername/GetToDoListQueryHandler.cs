using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, List<ToDoListVm>>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public GetToDoListQueryHandler(IToDoListRepository unitOfWork, IMapper mapper)
        {
            _toDoListRepository = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ToDoListVm>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ToDoList> toDoListResponse = await _toDoListRepository.GetToDoListsByUserName(request.Username);

            return _mapper.Map<List<ToDoListVm>>(toDoListResponse);
        }
    }
}
