using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentShoppingSession;

public record GetCurrentShoppingSessionQuery : IRequest<Result<GetCurrentShoppingSessionResponse>>;