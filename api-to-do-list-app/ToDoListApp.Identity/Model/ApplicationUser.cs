using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.Identity.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;
    }
}
