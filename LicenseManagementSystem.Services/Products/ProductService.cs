using AutoMapper;
using LicenseManagementSystem.DA;
using LicenseManagementSystem.Models.Models;
using Microsoft.EntityFrameworkCore;


namespace LicenseManagementSystem.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataBaseContext _context;


        public ProductService(IMapper mapper, DataBaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public List<ProductModel> GetAllProducts()
        {
            return _mapper.Map<List<ProductModel>>(_context.products.ToList());
        }

        public ProductModel GetProductById(long id)
        {
            return _mapper.Map<ProductModel>(_context.products.FirstOrDefault(x => x.Id == id));
        }

        public async Task<string> CreateProduct(ProductModel product)
        {
            var newProduct = _mapper.Map<Product>(product);
            newProduct.CreatedDate = DateTime.Now;
            await _context.products.AddAsync(newProduct);

            return _context.SaveChanges().ToString();
        }

        public async Task<string> UpdateProduct(ProductModel product)
        {
            var update = _mapper.Map<Product>(await _context.products.FirstOrDefaultAsync(x => x.Id == product.Id));
            update.Version = product.Version;
            update.ModifiedDate = DateTime.Now;
            update.Name = product.Name;
            update.Description = product.Description;
            _context.products.Update(update);

            return _context.SaveChanges().ToString();
        }

        public async Task<string> DeleteProductById(long id)
        {
            var delete = _mapper.Map<Product>(await _context.products.FirstOrDefaultAsync(x => x.Id == id));

            _context.products.Remove(delete);

            return _context.SaveChanges().ToString();
        }

    }
}
