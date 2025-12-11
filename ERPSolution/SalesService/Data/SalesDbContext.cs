using Microsoft.EntityFrameworkCore;
using SalesService.Models;

namespace SalesService.Data
{
    public class SalesDbContext: DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
       : base(options) { }

        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrder>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<SalesOrderItem>()
                .HasKey(i => i.ItemId);

            modelBuilder.Entity<SalesOrder>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId);
        }

    }
}
