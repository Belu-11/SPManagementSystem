using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases
{
    public class RecordTransactionsUseCase : IRecordTransactionsUseCase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IProductRepository productRepository;

        public RecordTransactionsUseCase(ITransactionRepository transactionRepository, IProductRepository productRepository)
        {
            this.transactionRepository = transactionRepository;
            this.productRepository = productRepository;
        }

        public void Execute(string cashierName, int productId, int quantity)
        {
            var product = productRepository.GetProductById(productId);
            if (product != null)
            {
                transactionRepository.Add(
                    cashierName,
                    productId,
                    product.Name,
                    product.Price.HasValue ? product.Price.Value : 0,
                    product.Quantity.HasValue ? product.Quantity.Value : 0,
                    quantity);
            }
        }
    }
}
