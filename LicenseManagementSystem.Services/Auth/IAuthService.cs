using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Login;
using LicenseManagementSystem.Models.Models;

namespace LicenseManagementSystem.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse> Login(LoginRequest request);
        Task<ApiResponse> RegisterUser(UserModel user);
    }
}