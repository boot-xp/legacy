using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
    public class UsersController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}