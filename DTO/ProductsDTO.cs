namespace ProductsAPI.Dto
{
    public class ProductsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}