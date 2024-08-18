using LicenseManagementSystem.Models;
using LicenseManagementSystem.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            var response = new ApiResponse
            {
                Message = "Users retrieved successfully",
                Response = users,
                Status = 200
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _userService.GetUserByIdService(id);

            if (user == null)
            {
                var notFoundResponse = new ApiResponse
                {
                    Message = "User not found",
                    Response = new object(),
                    Status = 404
                };
                return NotFound(notFoundResponse);
            }

            var response = new ApiResponse
            {
                Message = "User retrieved successfully",
                Response = user,
                Status = 200
            };

            return Ok(response);
        }
    }
}
