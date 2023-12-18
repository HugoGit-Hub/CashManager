using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Transactions.ValidateTransaction;

public record ValidateTransactionCommand(ValidateTransactionRequest ValidateTransactionRequest) : IRequest<Result>;