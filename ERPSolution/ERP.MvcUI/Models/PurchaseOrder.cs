namespace ERP.MvcUI.Models
{
    public class PurchaseOrder
    {
        public string PurchaseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<PurchaseItem> Items { get; set; }
    }
    public class PurchaseItem
    {
        public string ItemId { get; set; }
        public string PurchaseId { get; set; }
        public string ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
