using CashManager.Banking.Domain.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashManager.Banking.Domain.Accounts;

public class Account
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Owner { get; set; } = null!;

    [Required]
    [MaxLength(34)]
    public string Number { get; set; } = null!;

    [Required]
    public BankAccountEnum Bank { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime OpenDateTime { get; set; }

    [DataType(DataType.Date)]
    public DateTime CloseDateTime { get; set; }

    [ForeignKey(nameof(FkUser))]
    public int FkUser { get; set; }

    public Users User { get; set; } = null!;
}