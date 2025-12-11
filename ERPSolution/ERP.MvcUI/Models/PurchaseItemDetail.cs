namespace ERP.MvcUI.Models
{
    public class PurchaseItemDetail
    {
        public Guid ItemId { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
