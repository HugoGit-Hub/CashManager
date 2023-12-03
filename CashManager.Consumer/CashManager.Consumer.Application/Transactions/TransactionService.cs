using CashManager.Consumer.Domain.HttpClients;
using CashManager.Consumer.Domain.Transactions;

namespace CashManager.Consumer.Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IHttpClientService _httpClientService;

    public TransactionService(
        ITransactionRepository transactionRepository, 
        IHttpClientService httpClientService)
    {
        _transactionRepository = transactionRepository;
        _httpClientService = httpClientService;
    }

    public async Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken)
    {
        var result =  await _transactionRepository.Post(transaction, cancellationToken);
        await _httpClientService.Post(transaction, cancellationToken);

        return result;
    }

    public async Task<Transaction> Put(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = await Get(transaction.Guid, cancellationToken);
        result.State = TransactionStateEnum.Success;

        return await _transactionRepository.Put(result, cancellationToken);
    }

    public async Task<Transaction> Get(Guid guid, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.Get(guid, cancellationToken) ?? throw new NullTransactionException();

        return result;
    }
}
