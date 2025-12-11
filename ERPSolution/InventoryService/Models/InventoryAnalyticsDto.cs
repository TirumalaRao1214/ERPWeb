namespace InventoryService.Models
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
}
