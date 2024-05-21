using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;
using SPManagementSystem.ViewModels;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;

namespace SPManagementSystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly ISellProductUseCase sellProductUseCase;

        public SalesController(
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            ISellProductUseCase sellProductUseCase)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.sellProductUseCase = sellProductUseCase;
        }
        public IActionResult Index()
        {
            var salesVewModel = new SalesViewModel
            {
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(salesVewModel);
        }

        public IActionResult SellProductPartial(int productId)
        {
            var product = viewSelectedProductUseCase.Execute(productId);
            return PartialView("_SellProduct", product);
        }


        [HttpPost]
        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                sellProductUseCase.Execute(
                    "Cashier1",
                    salesViewModel.SelectedProductId,
                    salesViewModel.QuantityToSell);
            }

            var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
            salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
            salesViewModel.Categories = viewCategoriesUseCase.Execute();
            return View("Index", salesViewModel);
        }
    }
}
        