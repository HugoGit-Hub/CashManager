using CashManager.Banking.Domain.Accounts;

namespace CashManager.Banking.Application.Accounts;

public record GetAccountsByCurrentUserResponse(IEnumerable<Account> Accounts);
