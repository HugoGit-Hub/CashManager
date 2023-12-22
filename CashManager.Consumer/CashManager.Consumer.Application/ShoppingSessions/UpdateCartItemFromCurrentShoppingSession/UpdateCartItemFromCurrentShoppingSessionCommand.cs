using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.UpdateCartItemFromCurrentShoppingSession;

public record UpdateCartItemFromCurrentShoppingSessionCommand(int CartItemId, int Quantity) : IRequest<Result>;