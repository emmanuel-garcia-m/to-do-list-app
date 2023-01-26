using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.TodoList.Command.CreateToDoList;
using ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername;
using ToDoListApp.Application.Mapping;
using ToDoListApp.Application.UnitTest.Mocks;
using ToDoListApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoListApp.Application.UnitTest.Features.TodoList.Command.CreateToDoList
{
    public class CreateToDoListCommandHandlerXUnitTest
    {        
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CreateToDoListCommandHandler>> _logger;

        public CreateToDoListCommandHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            _logger = new Mock<ILogger<CreateToDoListCommandHandler>>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockToDoListRepository.AddDataRepository(_unitOfWork.Object.ToDoListdbContext);
        }

        [Fact]
        public async Task CreateToDoListCommandTest()
        {
            var handler = new CreateToDoListCommandHandler(_unitOfWork.Object, _unitOfWork.Object.ToDoListRepository, _mapper, _logger.Object);
            var request = new CreateToDoListCommand();
            request.description = "Primera lissta de tareas";

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<ToDoListVm>();

            result.ShouldNotBeNull();
        }
    }
}
