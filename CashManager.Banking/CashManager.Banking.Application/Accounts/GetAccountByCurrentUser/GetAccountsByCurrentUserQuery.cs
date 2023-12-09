using CashManager.Banking.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Banking.Application.Accounts.GetAccountByCurrentUser;

public record GetAccountsByCurrentUserQuery : IRequest<Result<IEnumerable<AccountResponse>>>;
