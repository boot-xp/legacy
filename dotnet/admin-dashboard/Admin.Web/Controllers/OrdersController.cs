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

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            return RedirectToAction("Index");
        }
    }
}