using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Application.Features.TodoList.Queries.GetToDoListByUsername
{
    public class GetToDoListQuery : IRequest<List<ToDoListVm>>
    {
        [Required]
        public string Username { get; set; } = String.Empty;

        public GetToDoListQuery(string username )
        {
            Username = username;
        }
    }
}
