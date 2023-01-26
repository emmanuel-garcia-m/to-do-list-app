using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoListApp.Application.Contracts.Identity;
using ToDoListApp.Application.Models.Identiry;

namespace ToDoListApp.WebApi.Controllers
{
    /// <summary>
    /// Accounts Administrator
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Get a jwt token for authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            AuthResponse response = await _authService.Login(request);

            if(response is null)
            {
                return Unauthorized("Credenciales invalidas");
            }
            else
            {
                return Ok(await _authService.Login(request));
            }           
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
    }
}
