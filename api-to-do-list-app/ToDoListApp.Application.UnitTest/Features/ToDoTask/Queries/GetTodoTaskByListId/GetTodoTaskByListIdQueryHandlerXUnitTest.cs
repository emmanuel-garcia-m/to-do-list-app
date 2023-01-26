using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.ToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Queries.GetTodoTaskByListId;
using ToDoListApp.Application.Mapping;
using ToDoListApp.Application.UnitTest.Mocks;
using ToDoListApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoListApp.Application.UnitTest.Features.ToDoTask.Queries.GetTodoTaskByListId
{
    public class GetTodoTaskByListIdQueryHandlerXUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetTodoTaskByListIdQueryHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockToDoTaskRepository.AddDataRepository(_unitOfWork.Object.ToDoListdbContext);
        }

        [Fact]
        public async Task GetTodoTaskByListIdQuery()
        {
            GetTodoTaskByListIdQueryHandler handler = new GetTodoTaskByListIdQueryHandler(_unitOfWork.Object.ToDoTaskRepository, _mapper);
            GetTodoTaskByListIdQuery request = new GetTodoTaskByListIdQuery(1);

            List<ToDoTaskVm> result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<ToDoTaskVm>>();

            result.Count.ShouldBe(1);
            result.FirstOrDefault().IsCompleted.ShouldBeFalse();
        }
    }
}
