using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using ToDoListApp.Infrastructure.Context;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ToDoListdbContext>()
                .UseInMemoryDatabase(databaseName: $"ToDoListdbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new ToDoListdbContext(options);
            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);

            return mockUnitOfWork;
        }
    }
}
