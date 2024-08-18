using LicenseManagementSystem.Controllers;
using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductsController _productsController;

        [SetUp]
        public void SetUp()
        {
            _productServiceMock = new Mock<IProductService>();
            _productsController = new ProductsController(_productServiceMock.Object);
        }

        [Test]
        public void GetAllProducts_ReturnsOk_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductModel> { /* Initialize list */ };
            _productServiceMock.Setup(s => s.GetAllProducts()).Returns(products);

            // Act
            var result = _productsController.GetAllProducts() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(products, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public void GetProductById_ReturnsOk_WhenProductIsFound()
        {
            // Arrange
            var productId = 1;
            var product = new ProductModel { /* Initialize properties */ };
            _productServiceMock.Setup(s => s.GetProductById(productId)).Returns(product);

            // Act
            var result = _productsController.GetProductById(productId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(product, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public void GetProductById_ReturnsNotFound_WhenProductIsNotFound()
        {
            // Arrange
            var productId = 1;
            _productServiceMock.Setup(s => s.GetProductById(productId)).Returns((ProductModel)null);

            // Act
            var result = _productsController.GetProductById(productId) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Product not found", ((ApiResponse)result.Value).Message);
        }

        [Test]
        public async Task CreateProduct_ReturnsCreatedAtAction_WhenProductIsCreatedSuccessfully()
        {
            // Arrange
            var product = new ProductModel { Id = 1, /* Initialize other properties */ };
            _productServiceMock.Setup(s => s.UpdateProduct(It.IsAny<ProductModel>())).ReturnsAsync("1");

            // Act
            var result = await _productsController.CreateProduct(product) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(product, ((ApiResponse)result.Value).Response);
            Assert.AreEqual(nameof(_productsController.GetProductById), result.ActionName);
            Assert.AreEqual(product.Id, result.RouteValues["id"]);
        }

        [Test]
        public async Task UpdateProduct_ReturnsOk_WhenProductIsUpdatedSuccessfully()
        {
            // Arrange
            var product = new ProductModel { Id = 1, /* Initialize other properties */ };
            _productServiceMock.Setup(s => s.UpdateProduct(It.IsAny<ProductModel>())).ReturnsAsync("1");

            // Act
            var result = await _productsController.UpdateProduct(product.Id, product) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(product, ((ApiResponse)result.Value).Response);
        }

        [Test]
        public async Task UpdateProduct_ReturnsBadRequest_WhenProductIdMismatch()
        {
            // Arrange
            var product = new ProductModel { Id = 1, /* Initialize other properties */ };

            // Act
            var result = await _productsController.UpdateProduct(2, product) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Product ID mismatch", ((ApiResponse)result.Value).Message);
        }

        [Test]
        public async Task DeleteProduct_ReturnsOk_WhenProductIsDeletedSuccessfully()
        {
            // Arrange
            var productId = 1;
            _productServiceMock.Setup(s => s.DeleteProductById(productId)).ReturnsAsync("1");

            // Act
            var result = await _productsController.DeleteProduct(productId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(true, ((ApiResponse)result.Value).Response);
        }
    }
}
