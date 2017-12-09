using System.Linq;
using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AdminContext _context;

        public CustomersController(AdminContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
                LastName = viewModel.LastName
            });
            await _context.SaveChangesAsync();
            return View("Index");
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
            return View("Index");
        }
    }
}