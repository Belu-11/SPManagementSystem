using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;
using SPManagementSystem.ViewModels;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;

namespace SPManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IAddProductUseCase addProductUseCase;
        private readonly IEditProductUseCase editProductUseCase;
        private readonly IDeleteProductUseCase deleteProductUseCase;
        private readonly IViewProductsUseCase viewProductsUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;

        public ProductsController(
            IAddProductUseCase addProductUseCase,
            IEditProductUseCase editProductUseCase,
            IDeleteProductUseCase deleteProductUseCase,
            IViewProductsUseCase viewProductsUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            IViewProductsInCategoryUseCase viewProductsInCategoryUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase)
        {
            this.addProductUseCase = addProductUseCase;
            this.editProductUseCase = editProductUseCase;
            this.deleteProductUseCase = deleteProductUseCase;
            this.viewProductsUseCase = viewProductsUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
        }
        public IActionResult Index()
        {
            var products = viewProductsUseCase.Execute(loadCategory:true);
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";

            var productViewModel = new ProductViewModel
            {
                Product = viewSelectedProductUseCase.Execute(id) ?? new CoreBusiness.Product(),
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                editProductUseCase.Execute(productViewModel.Product.ProductId, productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            productViewModel.Categories = viewCategoriesUseCase.Execute();
            return View(productViewModel);
        }
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var productViewModel = new ProductViewModel
            {
                Categories = viewCategoriesUseCase.Execute()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                addProductUseCase.Execute(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            productViewModel.Categories = viewCategoriesUseCase.Execute(); 
            return View(productViewModel);
        }

        public IActionResult Delete(int id)
        {
            deleteProductUseCase.Execute(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductByCategoryPartial(int categoryId)
        {
            var products = viewProductsInCategoryUseCase.Execute(categoryId);

            return PartialView("_Products", products);
        }

        public IActionResult ProductDetailsPartial(int productId)
        {
            var product = viewSelectedProductUseCase.Execute(productId);
            return PartialView("_ProductDetails", product);
        }
    }
}
