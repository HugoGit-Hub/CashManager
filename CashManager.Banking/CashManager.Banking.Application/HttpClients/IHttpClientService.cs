using CashManager.Banking.Application.Transactions;
using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Application.HttpClients;

public interface IHttpClientService
{
    public Task<Result> ValidateTransactionCallBack(ValidateTransactionCallBackRequest transaction, CancellationToken cancellationToken);
}