using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class TransactionInMemoryRepository : ITransactionRepository
    {
        private static List<Transaction> _transactions = new List<Transaction>();
        public void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
        {
            var transaction = new Transaction
            {
                ProductId = productId,
                ProductName = productName,
                TimeStamp = DateTime.Now,
                Price = price,
                BeforeQty = beforeQty,
                SoldQty = soldQty,
                CashierName = cashierName
            };

            if (_transactions != null && _transactions.Count > 0)
            {
                var maxId = _transactions.Max(x => x.TransactionId);
                transaction.TransactionId = maxId + 1;
            }
            else
            {
                transaction.TransactionId = 1;
            }

            _transactions?.Add(transaction);
        }

        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return _transactions.Where(x => x.TimeStamp.Date == date);
            else
                return _transactions.Where(x =>
                    x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                    x.TimeStamp.Date == date.Date);
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return _transactions.Where(x => x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date);
            else
                return _transactions.Where(x =>
                    x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                    x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date);
        }
    }
}
