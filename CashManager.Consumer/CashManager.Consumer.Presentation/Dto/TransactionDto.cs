using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Presentation.Dto;

public record TransactionDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(34)]
    public string Creditor { get; set; } = null!;
    
    [Required]
    public int Type { get; set; }

    [Required]
    public double Amount { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public Guid Guid { get; set; }
}