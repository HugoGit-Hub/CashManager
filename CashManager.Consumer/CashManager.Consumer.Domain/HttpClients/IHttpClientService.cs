using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.Transactions;

namespace CashManager.Consumer.Domain.HttpClients;

public interface IHttpClientService
{
    public Task<Result> Post(Transaction transaction, CancellationToken cancellationToken);
}