using CashManager.Consumer.Domain.Transaction;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Domain.Transactions;

public class Transaction
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

}