using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    public BankAccountEnum Bank { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public ICollection<Account> Accounts { get; } = new List<Account>();

    [JsonIgnore]
    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}