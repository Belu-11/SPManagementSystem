using Microsoft.AspNetCore.Mvc;

namespace SPManagementSystem.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
