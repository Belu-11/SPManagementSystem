using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;
using SPManagementSystem.ViewModels;

namespace SPManagementSystem.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
            return View(transactionsViewModel);
        }

        public IActionResult Search(TransactionsViewModel transactionsViewModel)
        {
            var transactons = TransactionsReposity.Search(
                transactionsViewModel.CashierName??string.Empty,
                transactionsViewModel.StartDate,
                transactionsViewModel.EndDate);

            transactionsViewModel.Transactions = transactons;

            return View("Index", transactionsViewModel);
        }
    }
}
