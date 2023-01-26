using System.Threading.Tasks;
using ToDoListApp.Application.Models.Identiry;

namespace ToDoListApp.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
