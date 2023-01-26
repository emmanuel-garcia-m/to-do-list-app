using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListApp.Identity.Model;

namespace ToDoListApp.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "e394ee1f-7341-4003-bcd7-d6a7cfedf84b",
                        Email = "admin@TodoListApp.com",
                        NormalizedEmail = "admin@TodoListApp.com",
                        Nombre = "Administrador",
                        Apellidos = "",
                        UserName = "Administrator",
                        NormalizedUserName = "administrator",
                        PasswordHash = hasher.HashPassword(null, "PassAdmin01"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "a46c4689-5897-4152-8410-aeed21c263a7",
                        Email = "emmanuelGarcia@TodoListApp.com",
                        NormalizedEmail = "emmanuegarcia@TodoListApp.com",
                        Nombre = "Emmanuel",
                        Apellidos = "Garcia",
                        UserName = "EmmanuelGarcia",
                        NormalizedUserName = "emmanuelgarcia",
                        PasswordHash = hasher.HashPassword(null, "PassEmma01"),
                        EmailConfirmed = true,
                    });
        }
    }
}
