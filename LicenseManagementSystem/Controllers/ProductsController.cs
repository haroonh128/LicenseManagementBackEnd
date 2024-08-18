using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var response = new ApiResponse
            {
                Message = "Products retrieved successfully",
                Response = _productService.GetAllProducts(),
                Status = 200
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(long id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                var notFoundResponse = new ApiResponse
                {
                    Message = "Product not found",
                    Response = new object(),
                    Status = 404
                };
                return NotFound(notFoundResponse);
            }

            var response = new ApiResponse
            {
                Message = "Product retrieved successfully",
                Response = product,
                Status = 200
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
        {
            var result = await _productService.CreateProduct(product);

            var response = new ApiResponse
            {
                Message = "Product created successfully",
                Response = result,
                Status = 201
            };

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductModel product)
        {
            if (id != product.Id)
            {
                var badRequestResponse = new ApiResponse
                {
                    Message = "Product ID mismatch",
                    Response = new object(),
                    Status = 400
                };
                return BadRequest(badRequestResponse);
            }

            var result = await _productService.UpdateProduct(product);

            var response = new ApiResponse
            {
                Message = "Product updated successfully",
                Response = result,
                Status = 200
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var result = await _productService.DeleteProductById(id);

            var response = new ApiResponse
            {
                Message = "Product deleted successfully",
                Response = result,
                Status = 200
            };

            return Ok(response);
        }
    }
}
