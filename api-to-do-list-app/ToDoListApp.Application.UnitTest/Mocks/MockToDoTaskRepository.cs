using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListApp.Domain.Models;
using ToDoListApp.Infrastructure.Context;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.Application.UnitTest.Mocks
{
    public static class MockToDoTaskRepository
    {
        public static Mock<ToDoTaskRepository> GetToDoListRepository()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ToDoListdbContext>()
                .UseInMemoryDatabase(databaseName: $"ToDoListdbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new ToDoListdbContext(options);
            streamerDbContextFake.Database.EnsureDeleted();

            return new Mock<ToDoTaskRepository>(streamerDbContextFake);
        }

        public static void AddDataRepository(ToDoListdbContext streamerDbContextFake)
        {
            Fixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            List<ToDoTask> toDoData = fixture.CreateMany<ToDoTask>().ToList();

            toDoData.Add(fixture.Build<ToDoTask>()
                .With(tr => tr.ToDoListId, 1)
                .With(tr => tr.IsCompleted, false)
                .Without(tr => tr.ToDoList)
                .Create()
            );

            toDoData.Add(fixture.Build<ToDoTask>()
                .With(tr => tr.Id, 1)
                .Create()
            );

            streamerDbContextFake.ToDoTasks!.AddRange(toDoData);
            streamerDbContextFake.SaveChanges();
        }
    }
}
