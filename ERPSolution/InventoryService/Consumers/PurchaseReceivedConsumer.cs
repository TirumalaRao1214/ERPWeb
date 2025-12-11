using InventoryService.Data;
using MassTransit;
using SharedKernel.Events;

namespace InventoryService.Consumers
{
    public class PurchaseReceivedConsumer: IConsumer<PurchaseReceived>
    {
        private readonly InventoryDbContext _db;

        public PurchaseReceivedConsumer(InventoryDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<PurchaseReceived> context)
        {
            foreach (var item in context.Message.Items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.CurrentStock += item.Qty;
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
