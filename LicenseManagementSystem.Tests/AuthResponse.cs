using LicenseManagementSystem.Models;

namespace LicenseManagementSystem.Tests
{
    internal class AuthResponse : ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}