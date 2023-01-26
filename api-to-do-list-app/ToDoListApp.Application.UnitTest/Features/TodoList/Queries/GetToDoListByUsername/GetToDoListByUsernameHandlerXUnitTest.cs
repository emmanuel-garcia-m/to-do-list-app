using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername;
using ToDoListApp.Application.Mapping;
using ToDoListApp.Application.UnitTest.Mocks;
using ToDoListApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoListApp.Application.UnitTest.Features.TodoList.Queries.GetToDoListByUsername
{
    public class GetToDoListByUsernameHandlerXUnitTest
    {
        private readonly IMapper _mapper;       
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetToDoListByUsernameHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();            

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockToDoListRepository.AddDataRepository(_unitOfWork.Object.ToDoListdbContext);
        }

        [Fact]
        public async Task GetToDoListQueryHandler()
        {
            var handler = new GetToDoListQueryHandler(_unitOfWork.Object.ToDoListRepository, _mapper);
            var request = new GetToDoListQuery("emmanuelmaestre");

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<ToDoListVm>>();

            result.Count.ShouldBe(1);
        }
    }
}
