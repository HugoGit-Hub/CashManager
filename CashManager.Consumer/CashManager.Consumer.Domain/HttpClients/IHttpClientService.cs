using CashManager.Consumer.Domain.Transactions;

namespace CashManager.Consumer.Domain.HttpClients;

public interface IHttpClientService
{
    public Task Post(Transaction transaction, CancellationToken cancellationToken);
}