using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.Transactions;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Domain.User;

public class Users
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Firstname { get; set; } = null!;

    [Required]
    [DataType(DataType.Text)]
    public string Lastname { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }

    public ICollection<Account> Accounts { get; } = new List<Account>();

    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}