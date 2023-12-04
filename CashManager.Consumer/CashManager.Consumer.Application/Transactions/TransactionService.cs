﻿using CashManager.Consumer.Domain.ErrorHandling;
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

    public async Task<Result<Transaction>> Post(Transaction transaction, CancellationToken cancellationToken)
    {
        var post =  await _transactionRepository.Post(transaction, cancellationToken);
        var result = await _httpClientService.Post(transaction, cancellationToken);
        
        return result.IsFailure 
            ? Result<Transaction>.Failure(result.Error)
            : Result<Transaction>.Success(post);
    }

    public async Task<Result<Transaction>> Put(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = await Get(transaction.Guid, cancellationToken);
        if (result.IsFailure)
        {
            return Result<Transaction>.Failure(result.Error);
        }
        
        result.Value.State = TransactionStateEnum.Success;

        var put = await _transactionRepository.Put(result.Value, cancellationToken);
        
        return Result<Transaction>.Success(put);
    }

    public async Task<Result<Transaction>> Get(Guid guid, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.Get(guid, cancellationToken);

        return result == null
            ? Result<Transaction>.Failure(TransactionErrors.TransactionNotFound)
            : Result<Transaction>.Success(result);
    }
}
