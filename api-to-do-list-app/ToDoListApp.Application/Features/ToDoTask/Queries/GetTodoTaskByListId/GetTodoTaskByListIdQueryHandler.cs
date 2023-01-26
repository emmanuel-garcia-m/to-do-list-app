using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Persistence;

namespace ToDoListApp.Application.Features.ToDoTask.Queries.GetTodoTaskByListId
{
    public class GetTodoTaskByListIdQueryHandler : IRequestHandler<GetTodoTaskByListIdQuery, List<ToDoTaskVm>>
    {
        private readonly IToDoTaskRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public GetTodoTaskByListIdQueryHandler(IToDoTaskRepository repository, IMapper mapper)
        {
            _toDoListRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<ToDoTaskVm>> Handle(GetTodoTaskByListIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Models.ToDoTask> toDoTaskListResponse = await _toDoListRepository.GetToDoTaskListsById(request.ToDoListId);

            return _mapper.Map<List<ToDoTaskVm>>(toDoTaskListResponse);
        }
    }
}
