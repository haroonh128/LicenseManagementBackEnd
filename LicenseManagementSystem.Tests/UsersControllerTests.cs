using LicenseManagementSystem.Controllers;
using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace LicenseManagementSystem.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UsersController _usersController;

        [SetUp]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _usersController = new UsersController(_userServiceMock.Object);
        }

        [Test]
        public async Task GetAllUsers_ReturnsOk_WithListOfUsers()
        {
            // Arrange
            var users = new List<UserModel> { /* Initialize list */ };
            _userServiceMock.Setup(s => s.GetAllUsers()).ReturnsAsync(users);

            // Act
            var result = await _usersController.GetAllUsers() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(users, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public async Task GetUserById_ReturnsOk_WhenUserIsFound()
        {
            // Arrange
            var userId = 1;
            var user = new UserModel { /* Initialize properties */ };
            _userServiceMock.Setup(s => s.GetUserByIdService(userId)).ReturnsAsync(user);

            // Act
            var result = await _usersController.GetUserById(userId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(user, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public async Task GetUserById_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = 1;
            _userServiceMock.Setup(s => s.GetUserByIdService(userId)).ReturnsAsync((UserModel)null);

            // Act
            var result = await _usersController.GetUserById(userId) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("User not found", ((ApiResponse)result.Value).Message);
        }
    }
}
