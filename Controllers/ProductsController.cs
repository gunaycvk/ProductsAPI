using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Products = {
        "IPhone 14","Iphone 15","IPhone 16"
    };
        // localhost:500/api/products => GET
        [HttpGet]
        public string[] GetProducts()
        {
            return Products;

        }
        // localhost:500/api/products/1 => GET
        [HttpGet("{id}")]
        public string GetProducts(int id)
        {

            return Products[id];
        }

    }
}