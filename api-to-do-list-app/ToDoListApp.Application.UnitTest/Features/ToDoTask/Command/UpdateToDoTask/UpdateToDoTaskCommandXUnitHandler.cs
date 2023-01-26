using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.ToDoTask.Command.UpdateToDoTask;
using ToDoListApp.Application.Mapping;
using ToDoListApp.Application.UnitTest.Mocks;
using ToDoListApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoListApp.Application.UnitTest.Features.ToDoTask.Command.UpdateToDoTask
{
    public class UpdateToDoTaskCommandXUnitHandler
    {
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UpdateToDoTaskCommandHandler>> _logger;

        public UpdateToDoTaskCommandXUnitHandler()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            _logger = new Mock<ILogger<UpdateToDoTaskCommandHandler>>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockToDoTaskRepository.AddDataRepository(_unitOfWork.Object.ToDoListdbContext);
        }

        [Fact]
        public async Task UpdateToDoTaskCommandHandlerTest()
        {
            UpdateToDoTaskCommandHandler handler = new UpdateToDoTaskCommandHandler(_unitOfWork.Object, _unitOfWork.Object.ToDoTaskRepository, _mapper, _logger.Object);
            UpdateToDoTaskCommand request = new UpdateToDoTaskCommand();
            request.Id = 1;
            request.IsCompleted = true;
            request.description = "Primera tarea de la lista editada";

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
