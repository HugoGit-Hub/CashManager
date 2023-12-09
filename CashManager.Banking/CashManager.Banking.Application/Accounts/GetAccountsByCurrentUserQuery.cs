using CashManager.Banking.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Banking.Application.Accounts;

public record GetAccountsByCurrentUserQuery : IRequest<Result<GetAccountsByCurrentUserResponse>>;
