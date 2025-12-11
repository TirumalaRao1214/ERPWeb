namespace ERP.MvcUI.Models
{
    public class PurchaseListItem
    {
        public Guid PurchaseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<PurchaseItemDetail> Items { get; set; }
    }
}
