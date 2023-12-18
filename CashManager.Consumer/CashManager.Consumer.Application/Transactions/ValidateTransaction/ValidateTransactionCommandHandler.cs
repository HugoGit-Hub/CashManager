using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.Transactions;
using MediatR;

namespace CashManager.Consumer.Application.Transactions.ValidateTransaction;

internal class ValidateTransactionCommandHandler : IRequestHandler<ValidateTransactionCommand, Result>
{
    private readonly ITransactionService _transactionService;

    public ValidateTransactionCommandHandler(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task<Result> Handle(ValidateTransactionCommand request, CancellationToken cancellationToken)
    {
        var getTransaction = await _transactionService.Get(request.ValidateTransactionRequest.Guid, cancellationToken);
        if (getTransaction.IsFailure)
        {
            return Result.Failure(getTransaction.Error);
        }
        
        var transaction = new Transaction
        {
            Id = getTransaction.Value.Id,
            Amount = getTransaction.Value.Amount,
            Creditor = getTransaction.Value.Creditor,
            Date = getTransaction.Value.Date,
            Guid = getTransaction.Value.Guid,
            State = TransactionStateEnum.Success,
            Type = getTransaction.Value.Type,
            UserId = getTransaction.Value.UserId,
            User = getTransaction.Value.User
        };

        var updateTransaction = await _transactionService.Put(transaction, cancellationToken);
        
        return updateTransaction.IsFailure 
            ? Result.Failure(updateTransaction.Error) 
            : Result.Success();
    }
}