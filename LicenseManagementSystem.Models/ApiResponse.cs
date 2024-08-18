namespace LicenseManagementSystem.Models
{
    public class ApiResponse
    {
        public string Message { get; set; } = string.Empty;
        public Object Response { get; set; } = new object();
        public int Status { get; set; }
    }
}
