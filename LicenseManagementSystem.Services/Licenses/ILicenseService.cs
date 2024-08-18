using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Models;

namespace LicenseManagementSystem.Services.Licenses
{
    public interface ILicenseService
    {
        Task<string> ActiveteLicense(long userId, string key);
        Task<ApiResponse> CreateLicenseKey(LicenseModel request);
        List<LicenseModel> GetAllLicenses();
        LicenseModel GetLicensesById(long id);
    }
}