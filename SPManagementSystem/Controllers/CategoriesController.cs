using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;
using UseCases.CategoriesUseCases;

namespace SPManagementSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;
        private readonly IAddCategoryUseCase addCategoryUseCase;
        private readonly IEditCategoryUseCase editCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;
        public CategoriesController(
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
            IAddCategoryUseCase addCategoryUseCase,
            IEditCategoryUseCase editCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
            this.addCategoryUseCase = addCategoryUseCase;
            this.editCategoryUseCase = editCategoryUseCase;
            this.deleteCategoryUseCase = deleteCategoryUseCase;
        }
        public IActionResult Index()
        {
            var categories = viewCategoriesUseCase.Execute();
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var category = viewSelectedCategoryUseCase.Execute(id.HasValue?id.Value:0);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(CoreBusiness.Category category)
        {
            if(ModelState.IsValid)
            {
                editCategoryUseCase.Execute(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            return View(category);
        }


        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(CoreBusiness.Category category)
        {
            if(ModelState.IsValid)
            {
                addCategoryUseCase.Execute(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(category);
        }

        public IActionResult Delete(int categoryId)
        {
            var products = ProductsRepository.GetProductsByCategoryId(categoryId);
            if (products != null && products.Count() > 0)
            {
                ModelState.AddModelError(string.Empty, "Cannot delete the category because there are products that contain it.");
                var categories = viewCategoriesUseCase.Execute(); // Assuming you have a method to retrieve all categories
                return View("Index", categories);
            }

            deleteCategoryUseCase.Execute(categoryId);
            return RedirectToAction(nameof(Index));
        }
    }
}