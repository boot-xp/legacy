using System.Linq;
using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly AdminContext _context;

        public CustomersController(AdminContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToArrayAsync();
            return View(new IndexViewModel
            {
                Customers = customers
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            _context.Add(new Customer
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Address = new Address
                {
                    AddressLine1 = viewModel.AddressLine1,
                    City = viewModel.City,
                    Country = viewModel.Country,
                    State = viewModel.State,
                    ZipCode = viewModel.ZipCode
                }
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _context.Customers
                .Select(c => new EditViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).SingleOrDefaultAsync(c => c.Id == id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel viewModel)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            customer.FirstName = viewModel.FirstName;
            customer.LastName = viewModel.LastName;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("api/customers/{id:int}/orders")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            var orders = await _context.Orders.Select(o => new OrderViewModel
            {
                Date = o.OrderDate,
                Id = o.Id,
                Total = o.LineItems.Sum(l => l.Quantity * l.Product.Price)
            }).ToArrayAsync();
            return Ok(orders);
        }
    }
}