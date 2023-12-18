using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.CartItems.CreateCartItem;

public record CreateCartItemCommand(CreateCartItemRequest CreateCartItemRequest) : IRequest<Result<CreateCartItemResponse>>;