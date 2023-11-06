using CashManager.Banking.Domain.Accounts;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Presentation.Dto;

public record AccountDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Owner { get; set; } = null!;

    [Required]
    [MaxLength(34)]
    public string Number { get; set; } = null!;

    [Required]
    public int Bank { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime OpenDateTime { get; set; }

    [DataType(DataType.Date)]
    public DateTime CloseDateTime { get; set; }
}