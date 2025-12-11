using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data
{
    public class InventoryDbContext: DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options) { }

        public DbSet<InventoryService.Models.Product> Products { get; set; }
    }
}
