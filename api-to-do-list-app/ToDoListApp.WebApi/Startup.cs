using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using ToDoListApp.Application;
using ToDoListApp.Identity;
using ToDoListApp.Infrastructure;
using ToDoListApp.WebApi.Middeware;

namespace ToDoListApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "ToDoListApp.WebApi", 
                    Version = "v1" , 
                    Contact = new OpenApiContact() {
                                    Email = "ema18gm@gmail.com", 
                                    Name="Emmanuel Garcia", 
                                    Url = new Uri ("https://www.linkedin.com/in/emmanuelgarciam/")}
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                         },
                         new string[] {}
                     }
                });


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Add cors
            services.AddCors(
                 options => options.AddPolicy("CorsPolicyToDoListApp",
                 builder =>
                 builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()                   
                    .AllowAnyHeader()
                    .SetPreflightMaxAge(TimeSpan.FromMinutes(120)))
            );

            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.ConfigureIdentityServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoListApp.WebApi v1"));
            }

            app.UseRouting();


            app.UseAuthentication();
            app.UseMiddleware<ExceptionTodoListAppHandlerMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("CorsPolicyToDoListApp");
        }
    }
}
