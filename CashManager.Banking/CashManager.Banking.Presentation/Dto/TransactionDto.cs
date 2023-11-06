using CashManager.Banking.Domain.Transactions;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Presentation.Dto;

public record TransactionDto
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
    [DataType(DataType.Date)]
    public string Date { get; set; } = null!;
}