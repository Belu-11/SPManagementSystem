using Microsoft.AspNetCore.Mvc;

namespace SPManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
