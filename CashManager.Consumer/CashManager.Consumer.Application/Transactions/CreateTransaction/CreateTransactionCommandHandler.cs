﻿using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Application.HttpClients;
using CashManager.Consumer.Application.Transactions.CreateTransaction.Requests;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.Transactions;
using CashManager.Consumer.Domain.User;
using MediatR;
using System.Security.Claims;

namespace CashManager.Consumer.Application.Transactions.CreateTransaction;

internal class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result>
{
    private readonly ITransactionService _transactionService;
    private readonly IHttpClientService _httpClientService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public CreateTransactionCommandHandler(
        ITransactionService transactionService,
        IHttpClientService httpClientService,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _transactionService = transactionService;
        _httpClientService = httpClientService;
        _currentUserService = currentUserService;
        _userService = userService;
    }

    public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userService.GetUserByEmail(_currentUserService.GetClaim(ClaimTypes.Email).Value, cancellationToken);
        if (currentUser.IsFailure)
        {
            return Result.Failure(currentUser.Error);
        }

        var transaction = new Transaction
        {
            Creditor = request.TransactionRequest.Creditor,
            Type = request.TransactionRequest.Type,
            Amount = request.TransactionRequest.Amount,
            Date = request.TransactionRequest.Date,
            Guid = request.TransactionRequest.Guid,
            UserId = currentUser.Value.Id,
            User = currentUser.Value
        };

        var createdTransaction = await _transactionService.Create(transaction, cancellationToken);
        if (createdTransaction.IsFailure)
        {
            return Result.Failure(createdTransaction.Error);
        }

        var createBankingTransactionRequest = new CreateBankingTransactionRequest
        {
            Creditor = request.TransactionRequest.Creditor,
            Type = request.TransactionRequest.Type,
            Amount = request.TransactionRequest.Amount,
            Date = request.TransactionRequest.Date,
            Guid = request.TransactionRequest.Guid
        };

        var result = await _httpClientService.PostTransaction(createBankingTransactionRequest, cancellationToken);
        
        return result.IsFailure 
            ? Result.Failure(result.Error) 
            : Result.Success();
    }
}