using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Application.Transactions.ValidateTransaction;

public record ValidateTransactionResponse
{
    public int Id { get; set; }

    [Required]
    [MaxLength(34)]
    public string Creditor { get; set; } = null!;

    [Required]
    [MaxLength(34)]
    public string Debtor { get; set; } = null!;

    [Required]
    public int Type { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public int State { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    public Guid Guid { get; set; }

    [Required]
    public string Url { get; set; } = null!;
}