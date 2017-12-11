using System.Linq;
using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly AdminContext _context;

        public UsersController(AdminContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Users = await _context.Users.ToArrayAsync()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users
                .Select(u => new EditViewModel
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    Id = u.Id,
                    LastName = u.LastName,
                    Password = u.Password,
                    IsAdmin = u.IsAdmin
                })
                .SingleOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel viewModel)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            user.Email = viewModel.Email;
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Password = viewModel.Password;
            user.IsAdmin = viewModel.IsAdmin;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            _context.Add(new User
            {
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Password = viewModel.Password,
                IsAdmin = viewModel.IsAdmin
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}