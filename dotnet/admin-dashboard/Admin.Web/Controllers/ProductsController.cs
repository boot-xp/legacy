using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AdminContext _context;

        public ProductsController(AdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToArrayAsync();
            return View(new IndexViewModel
            {
                Products = products
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
            _context.Add(new Product
            {
                Cost = viewModel.Cost,
                Name = viewModel.Name,
                Price = viewModel.Price
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            return View(new EditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Price = product.Price
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel viewModel)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            product.Name = viewModel.Name;
            product.Cost = viewModel.Cost;
            product.Price = viewModel.Price;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpGet("api/products")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToArrayAsync();
            return Ok(products);
        }

        [HttpGet("api/products/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            return Ok(product);
        } 
    }
}