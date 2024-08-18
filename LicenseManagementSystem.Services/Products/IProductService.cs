using LicenseManagementSystem.Models.Models;

namespace LicenseManagementSystem.Services.Products
{
    public interface IProductService
    {
        Task<string> CreateProduct(ProductModel product);
        Task<string> DeleteProductById(long id);
        List<ProductModel> GetAllProducts();
        ProductModel GetProductById(long id);
        Task<string> UpdateProduct(ProductModel product);
    }
}