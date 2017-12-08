using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
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
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            return View(new EditViewModel
            {
                User = user
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == viewModel.User.Id);
            user.Email = viewModel.User.Email;
            user.FirstName = viewModel.User.FirstName;
            user.LastName = viewModel.User.LastName;
            user.Password = viewModel.User.Password;
            await _context.SaveChangesAsync();
            return View("Index");
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
                Password = viewModel.Password
            });
            await _context.SaveChangesAsync();
            return View("Index");
        }
    }
}