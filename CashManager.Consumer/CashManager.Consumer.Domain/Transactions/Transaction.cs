using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Domain.Transactions;

public class Transaction
{
    public Transaction()
    {
        Debtor = "1234567890123456789012345678901234";
        State = TransactionStateEnum.Pending;
        Url = "https://localhost:7270/api/Transaction/Validate";
    }

    [Key] public int Id { get; set; }

    [Required]
    [MaxLength(34)]
    public string Creditor { get; set; } = null!;

    [Required]
    [MaxLength(34)]
    public string Debtor { get; private set; }

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

    [Required] 
    public string Url { get; private set; }
}
