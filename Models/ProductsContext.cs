using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ProductsAPI.Models

{
    public class ProductsContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 1, ProductName = "Iphone 14", Price = 60000, IsActive = true });
        }
        public DbSet<Product> Products { get; set; }

    }
}