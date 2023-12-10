using CashManager.Banking.Domain.Accounts;

namespace CashManager.Banking.Application.Accounts;

public record AccountResponse : AccoutBase
{
    public int Id { get; set; }
}
