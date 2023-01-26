using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoListApp.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "8dc2d17c-752e-44c3-98d0-ebbcf2e3646e",
                    UserId = "e394ee1f-7341-4003-bcd7-d6a7cfedf84b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "7a8f6a67-969e-4fd1-8078-8c019e09d309",
                    UserId = "a46c4689-5897-4152-8410-aeed21c263a7"
                }

            );
        }
    }
}
