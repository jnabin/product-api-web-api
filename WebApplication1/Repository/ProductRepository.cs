using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public async Task<List<Product>> GetAllProductAsync()
        {
            var jobs =await _productContext.Products.ToListAsync();
            return jobs;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var job =await _productContext.Products.FindAsync(id);
            return job;
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            _productContext.Products.Add(product);
            await _productContext.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task UpdateProductAsync(int id, UpdateProductViewModel model)
        {
            var product = new Product
            {
                ProductId = id,
                Name = model.Name,
                Quantity = model.Quantity,
                Price = model.Price
            };
            _productContext.Products.Update(product);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var job = new Product
            {
                ProductId = id
            };
            _productContext.Products.Remove(job);
            await _productContext.SaveChangesAsync();

        }
    }
}
