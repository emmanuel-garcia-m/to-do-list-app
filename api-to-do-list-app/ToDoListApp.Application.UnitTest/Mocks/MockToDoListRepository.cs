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
    public static class MockToDoListRepository
    {
        public static Mock<ToDoListRepository> GetToDoListRepository()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ToDoListdbContext>()
                .UseInMemoryDatabase(databaseName: $"ToDoListdbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new ToDoListdbContext(options);
            streamerDbContextFake.Database.EnsureDeleted();            

            return new Mock<ToDoListRepository>(streamerDbContextFake);
        }

        public static void AddDataRepository(ToDoListdbContext streamerDbContextFake)
        {
            Fixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            List<ToDoList> toDoData = fixture.CreateMany<ToDoList>().ToList();

            toDoData.Add(fixture.Build<ToDoList>()
                .With(tr => tr.CreatedBy, "emmanuelmaestre")
                .Create()
            );

            streamerDbContextFake.ToDoLists!.AddRange(toDoData);
            streamerDbContextFake.SaveChanges();
        }
    }
}
