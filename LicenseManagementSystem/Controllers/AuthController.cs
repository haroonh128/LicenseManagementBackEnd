using LicenseManagementSystem.Models.Login;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel user)
        {
            var response = await _authService.RegisterUser(user);

            if (response.Status == 200)
            {
                return Ok(response);
            }
            else
            {
                response.Status = 400;
                response.Message = "User registration failed";
                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.Login(request);

            if (response.Status == 200)
            {
                return Ok(response);
            }
            else
            {
                response.Status = 401;
                response.Message = "Login failed";
                return Unauthorized(response);
            }
        }
    }
}
