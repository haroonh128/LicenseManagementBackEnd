using LicenseManagementSystem.Models.Models;

namespace LicenseManagementSystem.Services.Users
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserByIdService(long id);
    }
}