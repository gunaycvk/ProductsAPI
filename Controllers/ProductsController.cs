using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Dto;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductsContext _context;
        private static List<Product>? _products;
        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        // localhost:500/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            var products = await _context.Products
            .Select(p => ProductToDTO(p)).ToListAsync();

            return Ok(_products);

        }
        // localhost:500/api/products/1 => GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = await _context.Products.Where(i => i.ProductId == id).Select(p => ProductToDTO(p)).FirstOrDefaultAsync();

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);


        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product entity)
        {
            try
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Veritabanı hatası: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Genel hata: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {

            if (id != entity.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static ProductsDTO ProductToDTO(Product p)
        {
            var entity = new ProductsDTO();
            if (p != null)
            {
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return new ProductsDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
            };
        }



    }
}