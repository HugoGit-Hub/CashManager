using CashManager.Banking.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Banking.Application.Transactions.ValidateTransaction;

public record ValidateTransactionCommand(ValidateTransactionRequest ValidateTransactionRequest) : IRequest<Result<ValidateTransactionResponse>>;