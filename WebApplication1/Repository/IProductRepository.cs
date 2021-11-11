using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductAsync();
        Task<int> CreateProductAsync(Product job);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(int id, UpdateProductViewModel model);
        Task DeleteProductAsync(int id);
    }
}
