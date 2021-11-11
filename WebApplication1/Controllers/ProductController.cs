using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository jobRepository)
        {
            _productRepository = jobRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var jobs =await _productRepository.GetAllProductAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var job =await _productRepository.GetProductByIdAsync(id);
            return Ok(job);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody]Product product)
        {
            var productId =await _productRepository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = productId, controller = "Product" }, productId);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel product, [FromRoute] int id)
        {
            await _productRepository.UpdateProductAsync(id, product);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return Ok();
        }
    }
}
