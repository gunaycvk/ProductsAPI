using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ProductsAPI.Models

{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {

        }

    }
}