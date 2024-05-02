using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;

namespace SPManagementSystem.Models.ViewComponents
{
    [ViewComponent]
    public class TransactionsViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string userName)
        {
            var transactions = TransactionsReposity.GetByDayCashier(userName, DateTime.Now);
            return View(transactions);
        }
    }
}
