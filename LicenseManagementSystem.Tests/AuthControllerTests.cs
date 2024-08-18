
using LicenseManagementSystem.Controllers;
using LicenseManagementSystem.Models.Login;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private AuthController _authController;

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Test]
        public async Task RegisterUser_ReturnsOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var user = new UserModel { /* Initialize user model properties */ };
            var expectedResponse = new AuthResponse { Status = 200, Message = "Registration successful" };

            _authServiceMock.Setup(s => s.RegisterUser(user)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.RegisterUser(user) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [Test]
        public async Task RegisterUser_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var user = new UserModel { /* Initialize user model properties */ };
            var expectedResponse = new AuthResponse { Status = 400, Message = "User registration failed" };

            _authServiceMock.Setup(s => s.RegisterUser(It.IsAny<UserModel>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.RegisterUser(user) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [Test]
        public async Task Login_ReturnsOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var request = new LoginRequest { /* Initialize login request properties */ };
            var expectedResponse = new AuthResponse { Status = 200, Message = "Login successful" };

            _authServiceMock.Setup(s => s.Login(request)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.Login(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [Test]
        public async Task Login_ReturnsUnauthorized_WhenLoginFails()
        {
            // Arrange
            var request = new LoginRequest { /* Initialize login request properties */ };
            var expectedResponse = new AuthResponse { Status = 401, Message = "Login failed" };

            _authServiceMock.Setup(s => s.Login(It.IsAny<LoginRequest>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.Login(request) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(expectedResponse, result.Value);
        }
    }
}
