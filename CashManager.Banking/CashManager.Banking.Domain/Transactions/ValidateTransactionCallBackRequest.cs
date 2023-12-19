using CashManager.Banking.Domain.Transactions;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Application.Transactions;

public record ValidateTransactionCallBackRequest
{
    public int Id { get; set; }

    [Required]
    [MaxLength(34)]
    public string Creditor { get; set; } = null!;

    [Required]
    [MaxLength(34)]
    public string Debtor { get; set; } = null!;

    [Required]
    public TransactionTypeEnum Type { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public TransactionStateEnum State { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    public Guid Guid { get; set; }

    public string? Signature { get; set; }

    [Required]
    public string Url { get; set; } = null!;
}