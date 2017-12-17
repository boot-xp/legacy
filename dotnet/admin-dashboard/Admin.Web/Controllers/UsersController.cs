using System.Linq;
using System.Threading.Tasks;
using Admin.Web.Models;
using Admin.Web.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Controllers
{
    [Authorize]
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
                    FirstName = u.FirstName,
                    Id = u.Id,
                    LastName = u.LastName,
                    IsAdmin = u.IsAdmin
                })
                .SingleOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel viewModel)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.IsAdmin = viewModel.IsAdmin;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}