using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ToDoListApp.Application.PersonalizedExceptions;

namespace ToDoListApp.WebApi.Middeware
{
    public class ExceptionTodoListAppHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionTodoListAppHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionTodoListAppHandlerMiddleware(RequestDelegate next, ILogger<ExceptionTodoListAppHandlerMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                /// se guarda log del error
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";                
                var result = string.Empty;

                if(ex is NotFoundException)
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                if (_env.IsDevelopment())
                    result = JsonConvert.SerializeObject(new { CodeError = context.Response.StatusCode, DevMessage = ex.Message, StackTrace = ex.StackTrace});                

                await context.Response.WriteAsync(result);
            }
        }
    }
}
