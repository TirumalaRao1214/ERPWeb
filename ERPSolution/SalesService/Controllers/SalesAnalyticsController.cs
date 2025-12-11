using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Models;

namespace SalesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesAnalyticsController : ControllerBase
    {
        private readonly SalesDbContext _db;

        public SalesAnalyticsController(SalesDbContext db)
        {
            _db = db;
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics()
        {
            var dto = new SalesAnalyticsDto();

            var today = DateTime.UtcNow.Date;

            // TODAY SALES (SAFE)
            dto.TodaySales = await _db.SalesOrders
                .Where(o => o.CreatedAt >= today && o.CreatedAt < today.AddDays(1))
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            // TOTAL ORDERS (SAFE)
            dto.TotalOrders = await _db.SalesOrders.CountAsync();

            // 1. Load raw monthly data (EF-safe query)
            var monthlyData = await _db.SalesOrders
                .Select(o => new
                {
                    Year = o.CreatedAt.Year,
                    Month = o.CreatedAt.Month,
                    Amount = o.TotalAmount
                })
                .ToListAsync();

            // 2. Group in memory (LINQ-to-Objects -> no EF restrictions)
            dto.MonthlySalesTrend = monthlyData
                .GroupBy(x => new { x.Year, x.Month })
                .Select(g => new SalesMonthlyDto
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Total = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Month)
                .ToList();

            // BEST SELLING PRODUCTS (EF-safe)
            var bestSelling = await _db.SalesOrderItems
    .GroupBy(i => i.ProductId)
    .Select(g => new BestSellingProductDto
    {
        ProductId = g.Key,
        ProductName = g.Key.ToString(),    // temporary placeholder
        QuantitySold = g.Sum(x => x.Qty)   // FIXED
    })
    .OrderByDescending(x => x.QuantitySold)
    .Take(5)
    .ToListAsync();

            dto.BestSellingProducts = bestSelling;




            return Ok(dto);
        }

    }
}
