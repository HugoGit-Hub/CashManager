using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.GetShoppingSessionCartItems;

public record GetShoppingSessionCartItemsQuery(int Id) : IRequest<Result<IEnumerable<GetShoppingSessionCartItemsResponse>>>;