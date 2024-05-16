namespace UseCases.TransactionsUseCases
{
    public interface IRecordTransactionsUseCase
    {
        void Execute(string cashierName, int productId, int quantity);
    }
}