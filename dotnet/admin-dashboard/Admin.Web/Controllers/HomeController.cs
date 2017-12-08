using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}