using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Models;
using SalesService.Services.DTOs;
using SharedKernel.Events;

namespace SalesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly SalesDbContext _db;
        private readonly IPublishEndpoint _publish;


        public SalesController(SalesDbContext db, IPublishEndpoint publish)
        {
            _db = db;
            _publish = publish;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateSalesOrderDto dto)
        {
            var order = new SalesOrder
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                TotalAmount = dto.Items.Sum(i => i.Price * i.Qty),
                Items = dto.Items.Select(i => new SalesOrderItem
                {
                    ItemId = Guid.NewGuid(),
                    ProductId = i.ProductId,
                    Qty = i.Qty,
                    Price = i.Price
                }).ToList()
            };

            _db.SalesOrders.Add(order);
            await _db.SaveChangesAsync();
            await _publish.Publish(new OrderCreated
            {
                OrderId = order.OrderId,
                Items = order.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Qty = i.Qty
                }).ToList()
            });
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _db.SalesOrders.Include(o => o.Items).ToListAsync());
        }

    }
}
