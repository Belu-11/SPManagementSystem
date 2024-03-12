using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;

namespace SPManagementSystem.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            var categories = CategoriesRepository.GetCategories();
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            var category = CategoriesRepository.GetCategoryById(id.HasValue?id.Value:0);
            return View(category);
        }
    }
}