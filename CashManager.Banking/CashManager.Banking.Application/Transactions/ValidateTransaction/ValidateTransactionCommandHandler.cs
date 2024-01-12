using CashManager.Banking.Application.HttpClients;
using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.Transactions;
using Mapster;
using MediatR;

namespace CashManager.Banking.Application.Transactions.ValidateTransaction;

internal class ValidateTransactionCommandHandler : IRequestHandler<ValidateTransactionCommand, Result<ValidateTransactionResponse>>
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;
    private readonly IHttpClientService _httpClientService;

    public ValidateTransactionCommandHandler(
        ITransactionService transactionService,
        IAccountService accountService,
        IHttpClientService httpClientService)
    {
        _transactionService = transactionService;
        _accountService = accountService;
        _httpClientService = httpClientService;
    }

    public async Task<Result<ValidateTransactionResponse>> Handle(ValidateTransactionCommand request, CancellationToken cancellationToken)
    {
        var validateTransaction = request.ValidateTransactionRequest;
        var transaction = validateTransaction.Adapt<Transaction>();
        var validate = await _transactionService.ValidateOrAbort(transaction, TransactionStateEnum.Success, cancellationToken);
        if (validate.IsFailure)
        {
            return Result<ValidateTransactionResponse>.Failure(validate.Error);
        }

        var creditor = validate.Value.Creditor;
        var debtor = validate.Value.Debtor;
        var amount = validate.Value.Amount;
        var creditAndDebit = await _accountService.CreditAndDebit(creditor, debtor, amount, cancellationToken);
        if (creditAndDebit.IsFailure)
        {
            await _transactionService.ValidateOrAbort(transaction, TransactionStateEnum.Pending, cancellationToken);
            return Result<ValidateTransactionResponse>.Failure(creditAndDebit.Error);
        }

        var validateTransactionCallBackRequest = validateTransaction.Adapt<ValidateTransactionCallBackRequest>();
        var putTransaction = await _httpClientService.ValidateTransactionCallBack(validateTransactionCallBackRequest, cancellationToken);
        if (putTransaction.IsSuccess)
        {
            return Result<ValidateTransactionResponse>.Success(validateTransaction.Adapt<ValidateTransactionResponse>());
        }

        await _transactionService.ValidateOrAbort(transaction, TransactionStateEnum.Pending, cancellationToken);
        await _accountService.CreditAndDebit(creditor, debtor, -amount, cancellationToken);
        
        return Result<ValidateTransactionResponse>.Failure(putTransaction.Error);
    }
}