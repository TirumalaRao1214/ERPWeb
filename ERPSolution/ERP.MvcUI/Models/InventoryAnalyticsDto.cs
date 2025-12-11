namespace ERP.MvcUI.Models
{
    public class InventoryAnalyticsDto
    {
        public int TotalProducts { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
        public decimal TotalStockValue { get; set; }

        public List<StockItemDto> TopItems { get; set; }
    }

    public class StockItemDto
    {
        public string Name { get; set; }
        public int CurrentStock { get; set; }
    }

    public class SalesAnalyticsDto
    {
        public decimal TodaySales { get; set; }
        public int TotalOrders { get; set; }
        public decimal MonthlyRevenue { get; set; }

        public List<SalesMonthlyDto> MonthlySalesTrend { get; set; }
        public List<BestSellingProductDto> BestSellingProducts { get; set; }
    }

    public class SalesMonthlyDto
    {
        public string Month { get; set; }
        public decimal Total { get; set; }
    }

    public class BestSellingProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
    }

    public class PurchaseAnalyticsDto
    {
        public int TotalPurchaseOrders { get; set; }
        public int PendingGRN { get; set; }
        public decimal TotalMonthlyPurchases { get; set; }

        public List<MonthlyPurchaseDto> MonthlyPurchases { get; set; }
        public List<SupplierPerformanceDto> SupplierPerformance { get; set; }
    }

    public class MonthlyPurchaseDto
    {
        public string Month { get; set; }
        public decimal Total { get; set; }
    }

    public class SupplierPerformanceDto
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int OrdersPlaced { get; set; }
        public int OrdersDelivered { get; set; }
    }

}
