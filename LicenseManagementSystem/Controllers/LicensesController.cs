using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.ActivateLicenseRequest;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Licenses;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        private readonly ILicenseService _licenseService;

        public LicensesController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLicenseKey([FromBody] LicenseModel request)
        {
            var response = await _licenseService.CreateLicenseKey(request);

            if (response.Status == 200)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        public IActionResult GetAllLicenses()
        {
            var licenses = _licenseService.GetAllLicenses();
            var response = new ApiResponse
            {
                Message = "Licenses retrieved successfully",
                Response = licenses,
                Status = 200
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetLicensesById(long id)
        {
            var license = _licenseService.GetLicensesById(id);

            if (license == null)
            {
                var notFoundResponse = new ApiResponse
                {
                    Message = "License not found",
                    Response = new object(),
                    Status = 404
                };
                return NotFound(notFoundResponse);
            }

            var response = new ApiResponse
            {
                Message = "License retrieved successfully",
                Response = license,
                Status = 200
            };

            return Ok(response);
        }

        [HttpPost("activate")]
        public async Task<IActionResult> ActivateLicense([FromBody] ActivateLicenseRequest request)
        {
            var result = await _licenseService.ActiveteLicense(request.UserId, request.Key);

            var response = new ApiResponse
            {
                Message = result == "License Activated!" ? "License activated successfully" : "License activation failed",
                Response = result,
                Status = result == "License Activated!" ? 200 : 400
            };

            if (response.Status == 200)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}
