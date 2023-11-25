using CashManager.Banking.Domain.Transactions;

namespace CashManager.Banking.Domain.HttpClients;

public interface IHttpClientService
{
    public Task Post(Transaction transaction, CancellationToken cancellationToken);
}