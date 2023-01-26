using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.ToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Command.CreateToDoTask;
using ToDoListApp.Application.Mapping;
using ToDoListApp.Application.UnitTest.Mocks;
using ToDoListApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoListApp.Application.UnitTest.Features.ToDoTask.Command.CreateToDoTask
{
    public class CreateToDoTaskCommandHandlerXUnitTest
    {
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CreateToDoTaskCommandHandler>> _logger;

        public CreateToDoTaskCommandHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            _logger = new Mock<ILogger<CreateToDoTaskCommandHandler>>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockToDoTaskRepository.AddDataRepository(_unitOfWork.Object.ToDoListdbContext);
        }

        [Fact]
        public async Task CreateToDoTaskCommandTest()
        {
            CreateToDoTaskCommandHandler handler = new CreateToDoTaskCommandHandler(_unitOfWork.Object, _unitOfWork.Object.ToDoTaskRepository, _mapper, _logger.Object);
            CreateToDoTaskCommand request = new CreateToDoTaskCommand();
            request.description = "Primera tarea de la lista";

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<ToDoTaskVm>();

            result.ShouldNotBeNull();
            result.IsCompleted.ShouldBeFalse();
        }
    }
}
