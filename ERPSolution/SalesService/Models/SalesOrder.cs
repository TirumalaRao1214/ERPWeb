using System.ComponentModel.DataAnnotations;

namespace SalesService.Models
{
    public class SalesOrder
    {
        [Key]

        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SalesOrderItem> Items { get; set; }


        
    }
}
