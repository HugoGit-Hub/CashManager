using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.CreateShoppingSession;

public record CreateShoppingSessionCommand : IRequest<Result>;