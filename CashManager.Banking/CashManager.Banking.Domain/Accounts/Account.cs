using CashManager.Banking.Domain.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashManager.Banking.Domain.Accounts;

public record Account : AccoutBase
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }

    public Users User { get; set; } = null!;
}