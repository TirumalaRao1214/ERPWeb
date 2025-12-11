using InventoryService.Data;
using MassTransit;
using SharedKernel.Events;

namespace InventoryService.Consumers
{
    public class OrderCreatedConsumer: IConsumer<OrderCreated>
    {
        private readonly InventoryDbContext _db;

        public OrderCreatedConsumer(InventoryDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            foreach (var item in context.Message.Items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.CurrentStock -= item.Qty;
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
