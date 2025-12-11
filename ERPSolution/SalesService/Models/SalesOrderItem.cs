using System.ComponentModel.DataAnnotations;

namespace SalesService.Models
{
    public class SalesOrderItem
    {
        [Key]

        public Guid ItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
