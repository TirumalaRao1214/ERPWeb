namespace ERP.MvcUI.Models
{
    public class DashboardViewModel
    {
        public InventoryAnalyticsDto Inventory { get; set; }
        public SalesAnalyticsDto Sales { get; set; }
        public PurchaseAnalyticsDto Purchase { get; set; }
    }
}
