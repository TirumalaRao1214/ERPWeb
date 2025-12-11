namespace SalesService.Services.DTOs
{
    public class SalesOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
