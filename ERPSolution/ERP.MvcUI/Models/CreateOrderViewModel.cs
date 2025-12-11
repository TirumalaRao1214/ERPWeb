namespace ERP.MvcUI.Models
{
    public class CreateOrderViewModel
    {
        public List<SalesItemViewModel> Items { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }

    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int CurrentStock { get; set; }
    }

    public class SalesItemViewModel
    {
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
