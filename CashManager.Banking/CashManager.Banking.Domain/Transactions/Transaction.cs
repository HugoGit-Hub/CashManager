using CashManager.Banking.Domain.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashManager.Banking.Domain.Transactions;

public class Transaction
{
    [Key]
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

    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }

    public Users User { get; set; } = null!;
}