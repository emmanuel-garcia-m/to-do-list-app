using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.ToDoTask.Command.DeleteTodoTask;
using ToDoListApp.Application.Mapping;
using ToDoListApp.Application.UnitTest.Mocks;
using ToDoListApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoListApp.Application.UnitTest.Features.ToDoTask.Command.DeleteTodoTask
{
    public class DeleteToDoTaskCommandXUnitHandler
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<DeleteToDoTaskCommandHandler>> _logger;

        public DeleteToDoTaskCommandXUnitHandler()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            _logger = new Mock<ILogger<DeleteToDoTaskCommandHandler>>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockToDoTaskRepository.AddDataRepository(_unitOfWork.Object.ToDoListdbContext);
        }

        [Fact]
        public async Task GetToDoListQueryHandler()
        {
            var handler = new DeleteToDoTaskCommandHandler(_unitOfWork.Object, _unitOfWork.Object.ToDoTaskRepository, _mapper, _logger.Object);
            var request = new DeleteToDoTaskCommand();
            request.Id = 1;

            var result = await handler.Handle(request, CancellationToken.None); 
            result.ShouldBeOfType<Unit>();
        }
    }
}
