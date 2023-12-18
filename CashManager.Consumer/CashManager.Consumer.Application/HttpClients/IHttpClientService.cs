using CashManager.Consumer.Application.Transactions.CreateTransaction.Requests;
using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Application.HttpClients;

public interface IHttpClientService
{
    public Task<Result> PostTransaction(CreateBankingTransactionRequest createBankingTransactionRequest, CancellationToken cancellationToken);
}