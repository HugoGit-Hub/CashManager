using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.Transactions;

namespace CashManager.Banking.Domain.HttpClients;

public interface IHttpClientService
{
    public Task<Result> PutTransaction(Transaction transaction, CancellationToken cancellationToken);
}