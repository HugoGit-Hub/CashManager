using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;

public record GetShoppingSessionCartItemsQuery : IRequest<Result<IEnumerable<GetShoppingSessionCartItemsResponse>>>;