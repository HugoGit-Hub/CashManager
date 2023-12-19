using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.DeleteCartItemFromCurrentShoppingSession;

public record DeleteCartItemFromCurrentShoppingSessionCommand(int CartItemId) : IRequest<Result>;