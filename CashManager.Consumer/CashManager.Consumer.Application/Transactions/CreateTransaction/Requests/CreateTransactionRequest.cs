using CashManager.Consumer.Domain.Transactions;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.Transactions.CreateTransaction.Requests;

public record CreateTransactionRequest
{
    [Required]
    [MaxLength(34)]
    public string Creditor { get; set; } = null!;

    [Required]
    public TransactionTypeEnum Type { get; set; }
}