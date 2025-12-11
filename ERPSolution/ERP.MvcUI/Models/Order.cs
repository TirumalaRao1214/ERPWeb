namespace ERP.MvcUI.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
