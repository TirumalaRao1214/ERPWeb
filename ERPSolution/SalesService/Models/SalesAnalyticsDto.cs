namespace SalesService.Models
{
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
        public string Month { get; set; }   // "2025-01"
        public decimal Total { get; set; }
    }

    public class BestSellingProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
    }
}
