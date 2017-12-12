using System;
using System.Linq;
using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AdminContext _context;

        public OrdersController(AdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.LineItems)
                .ThenInclude(l => l.Product)
                .ToArrayAsync();
            return View(new IndexViewModel
            {
                Orders = orders,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CreateViewModel
            {
                Customers = await _context.Customers.ToArrayAsync()
            });
        }

        [HttpPost("api/orders")]
        public async Task<IActionResult> CreateOrder([FromBody] ApiCreateViewModel viewModel)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == viewModel.CustomerId);
            var products = await _context.Products
                .Where(p => viewModel.Items.Any(i => i.ProductId == p.Id))
                .ToArrayAsync();

            _context.Add(new Order
            {
                OrderDate = DateTime.UtcNow,
                Customer = customer,
                LineItems = products.Select(p => new OrderLineItem
                {
                    Product = p,
                    Quantity = viewModel.Items.First(i => i.ProductId == p.Id).Quantity
                }).ToArray()
            });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}