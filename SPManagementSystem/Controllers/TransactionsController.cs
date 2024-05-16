using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;
using SPManagementSystem.ViewModels;
using UseCases.TransactionsUseCases;

namespace SPManagementSystem.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ISearchTransactionsUseCase searchTransactionsUseCase;

        public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase)
        {
            this.searchTransactionsUseCase = searchTransactionsUseCase;
        }
        public IActionResult Index()
        {
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
            return View(transactionsViewModel);
        }

        public IActionResult Search(TransactionsViewModel transactionsViewModel)
        {
            var transactons = searchTransactionsUseCase.Execute(
                transactionsViewModel.CashierName??string.Empty,
                transactionsViewModel.StartDate,
                transactionsViewModel.EndDate);

            transactionsViewModel.Transactions = transactons;

            return View("Index", transactionsViewModel);
        }
    }
}
