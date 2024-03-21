using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;
using SPManagementSystem.ViewModels;

namespace SPManagementSystem.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            var salesVewModel = new SalesViewModel
            {
                Categories = CategoriesRepository.GetCategories()
            };

            return View(salesVewModel);
        }

        public IActionResult SellProductPartial(int productId)
        {
            var product = ProductsRepository.GetProductById(productId);
            return PartialView("_SellProduct", product);
        }
    }
}
        