namespace ERP.MvcUI.Models
{
    public class OrderItem
    {
        public string ItemId { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
