using InventoryService.Data;
using InventoryService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly InventoryDbContext _db;

        public ProductController(InventoryDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.ProductId = Guid.NewGuid();
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Products.ToListAsync());
        }

        [HttpGet("debug")]
        public IActionResult DebugHeader()
        {
            return Ok(new
            {
                auth = Request.Headers["Authorization"].FirstOrDefault()
            });
        }
    }
}
