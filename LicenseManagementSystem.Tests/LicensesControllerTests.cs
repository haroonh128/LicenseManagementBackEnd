using LicenseManagementSystem.Controllers;
using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.ActivateLicenseRequest;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Licenses;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace LicenseManagementSystem.Tests
{
    [TestFixture]
    public class LicensesControllerTests
    {
        private Mock<ILicenseService> _licenseServiceMock;
        private LicensesController _licensesController;

        [SetUp]
        public void SetUp()
        {
            _licenseServiceMock = new Mock<ILicenseService>();
            _licensesController = new LicensesController(_licenseServiceMock.Object);
        }

        [Test]
        public async Task CreateLicenseKey_ReturnsOk_WhenLicenseCreationIsSuccessful()
        {
            // Arrange
            var licenseRequest = new LicenseModel { /* Initialize properties */ };
            var expectedResponse = new ApiResponse { Status = 200, Message = "License created successfully" };

            _licenseServiceMock.Setup(s => s.CreateLicenseKey(licenseRequest)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _licensesController.CreateLicenseKey(licenseRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [Test]
        public async Task CreateLicenseKey_ReturnsBadRequest_WhenLicenseCreationFails()
        {
            // Arrange
            var licenseRequest = new LicenseModel { /* Initialize properties */ };
            var expectedResponse = new ApiResponse { Status = 400, Message = "License creation failed" };

            _licenseServiceMock.Setup(s => s.CreateLicenseKey(licenseRequest)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _licensesController.CreateLicenseKey(licenseRequest) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [Test]
        public void GetAllLicenses_ReturnsOk_WithListOfLicenses()
        {
            // Arrange
            var licenses = new List<LicenseModel> { /* Initialize list */ };
            _licenseServiceMock.Setup(s => s.GetAllLicenses()).Returns(licenses);

            // Act
            var result = _licensesController.GetAllLicenses() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(licenses, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public void GetLicensesById_ReturnsOk_WhenLicenseIsFound()
        {
            // Arrange
            var licenseId = 1;
            var license = new LicenseModel { /* Initialize properties */ };
            _licenseServiceMock.Setup(s => s.GetLicensesById(licenseId)).Returns(license);

            // Act
            var result = _licensesController.GetLicensesById(licenseId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(license, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public void GetLicensesById_ReturnsNotFound_WhenLicenseIsNotFound()
        {
            // Arrange
            var licenseId = 1;
            _licenseServiceMock.Setup(s => s.GetLicensesById(licenseId)).Returns((LicenseModel)null);

            // Act
            var result = _licensesController.GetLicensesById(licenseId) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("License not found", ((ApiResponse)result.Value).Message);
        }

        [Test]
        public async Task ActivateLicense_ReturnsOk_WhenActivationIsSuccessful()
        {
            // Arrange
            var request = new ActivateLicenseRequest { UserId = 1, Key = "valid-key" };
            var expectedResponse = "License Activated!";
            var expectedApiResponse = new ApiResponse { Status = 200, Message = "License activated successfully", Response = expectedResponse };

            _licenseServiceMock.Setup(s => s.ActiveteLicense(request.UserId, request.Key)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _licensesController.ActivateLicense(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedApiResponse, result.Value);
        }

        [Test]
        public async Task ActivateLicense_ReturnsBadRequest_WhenActivationFails()
        {
            // Arrange
            var request = new ActivateLicenseRequest { UserId = 1, Key = "invalid-key" };
            var expectedResponse = "License activation failed";
            var expectedApiResponse = new ApiResponse { Status = 400, Message = "License activation failed", Response = expectedResponse };

            _licenseServiceMock.Setup(s => s.ActiveteLicense(request.UserId, request.Key)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _licensesController.ActivateLicense(request) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(expectedApiResponse, result.Value);
        }
    }
}
