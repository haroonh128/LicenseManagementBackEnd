

namespace LicenseManagementSystem.Models.ActivateLicenseRequest
{
    public class ActivateLicenseRequest
    {
        public long UserId { get; set; }
        public string Key { get; set; } = string.Empty;
    }
}
