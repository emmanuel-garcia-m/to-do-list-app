using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Identity.Configuration;
using ToDoListApp.Identity.Model;

namespace ToDoListApp.Identity.Context
{
    public class ToDoListAppIdentityDbContext : IdentityDbContext<ApplicationUser> 
    {       
        public ToDoListAppIdentityDbContext(DbContextOptions<ToDoListAppIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
