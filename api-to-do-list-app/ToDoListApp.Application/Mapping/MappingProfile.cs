using AutoMapper;
using ToDoListApp.Application.Features.TodoList.Command.CreateToDoList;
using ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername;
using ToDoListApp.Application.Features.ToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Command.CreateToDoTask;
using ToDoListApp.Application.Features.ToDoTask.Command.UpdateToDoTask;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoList, ToDoListVm>();
            CreateMap<ToDoList, CreateToDoListCommand>().ReverseMap();
            CreateMap<ToDoTask, ToDoTaskVm>();
            CreateMap<ToDoTask, UpdateToDoTaskCommand>().ReverseMap();
            CreateMap<ToDoTask, CreateToDoTaskCommand>().ReverseMap();
        }
    }
}
