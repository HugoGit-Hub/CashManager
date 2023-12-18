using CashManager.Consumer.Application.Transactions.CreateTransaction.Requests;
using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Transactions.CreateTransaction;

public record CreateTransactionCommand(CreateTransactionRequest TransactionRequest) : IRequest<Result>;