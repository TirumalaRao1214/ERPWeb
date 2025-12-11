using InventoryService.Data;
using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryAnalyticsController : ControllerBase
    {
        private readonly InventoryDbContext _db;

        public InventoryAnalyticsController(InventoryDbContext db)
        {
            _db = db;
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics()
        {
            var dto = new InventoryAnalyticsDto
            {
                TotalProducts = await _db.Products.CountAsync(),
                LowStockItems = await _db.Products.Where(p => p.CurrentStock < 10).CountAsync(),
                OutOfStockItems = await _db.Products.Where(p => p.CurrentStock == 0).CountAsync(),
                TotalStockValue = await _db.Products.SumAsync(p => p.Price * p.CurrentStock),
                TopItems = await _db.Products
                            .OrderByDescending(p => p.CurrentStock)
                            .Take(5)
                            .Select(p => new StockItemDto
                            {
                                Name = p.Name,
                                CurrentStock = p.CurrentStock
                            }).ToListAsync()
            };

            return Ok(dto);
        }
    }
}
